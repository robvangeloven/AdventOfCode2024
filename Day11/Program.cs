using System.Collections.Concurrent;

var input = File
    .ReadAllText("input.txt")
    .Split(' ')
    .Select(long.Parse)
    .ToList();

var cache = new ConcurrentDictionary<(long number, int count), long>();

long Blink(long number, int times)
{
    if (times == 0)
    {
        return 1;
    }

    var cacheKey = (number, times);

    if (cache.TryGetValue(cacheKey, out var cachedValue))
    {
        return cachedValue;
    }

    var result = 0L;

    if (number == 0)
    {
        result = Blink(1, times - 1);
    }
    else
    {
        var digit = number.ToString();

        if (digit.Length % 2 == 0)
        {
            var half = digit.Length / 2;
            result = Blink(long.Parse(digit[0..half]), times - 1) + Blink(long.Parse(digit[half..]), times - 1);
        }
        else
        {
            result = Blink(number * 2024, times - 1);
        }
    }

    cache.AddOrUpdate(cacheKey, result, (_, value) => value);

    return result;
}

void PartOne()
{
    var answer = input
        .AsParallel()
        .Select(x => Blink(x, 25))
        .Sum();

    Console.WriteLine($"Answer part one: {answer}");
}

void PartTwo()
{
    var answer = input
        .AsParallel()
        .Select(x => Blink(x, 75))
        .Sum();

    Console.WriteLine($"Answer part two: {answer}");
}

PartOne();
PartTwo();

Console.ReadLine();
