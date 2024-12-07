var input = File.ReadAllLines("input.txt");

var sums = input.Select(x =>
{
    var values = x.Split(": ");
    var sum = values[0];
    var operands = values[1].Split(' ');

    return new
    {
        Sum = long.Parse(sum),
        Operands = operands.Select(long.Parse).ToArray(),
    };
});

bool ValidateSum(long sum, long[] operands, bool partTwo)
{
    if (operands.Length == 1)
    {
        return sum == operands[0];
    }

    return ValidateSum(sum, [operands[0] + operands[1], .. operands[2..]], partTwo) ||
        ValidateSum(sum, [operands[0] * operands[1], .. operands[2..]], partTwo) ||
        (partTwo && ValidateSum(sum, [long.Parse($"{operands[0]}{operands[1]}"), .. operands[2..]], partTwo));
}

void PartOne()
{
    long answer = 0;

    answer = sums
        .AsParallel()
        .Where(x => ValidateSum(x.Sum, x.Operands, false))
        .Sum(x => x.Sum);

    Console.WriteLine($"Answer part one: {answer}");
}

void PartTwo()
{
    long answer = 0;

    answer = sums
        .AsParallel()
        .Where(x => ValidateSum(x.Sum, x.Operands, true))
        .Sum(x => x.Sum);

    Console.WriteLine($"Answer part two: {answer}");
}

PartOne();
PartTwo();

Console.ReadLine();
