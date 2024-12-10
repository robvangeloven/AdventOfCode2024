
void PartOne()
{
    var emptyNodes = 0;
    var stack = new Stack<int>();

    var answer = File
        .ReadAllText("input.txt")
        .SelectMany((c, index) =>
        {
            var fileId = index / 2;

            return Enumerable
               .Range(0, int.Parse($"{c}"))
               .Select(_ =>
               {
                   if (index % 2 == 0)
                   {
                       stack.Push(fileId);
                       return fileId;
                   }

                   emptyNodes++;
                   return (int?)null;
               });

        })
        .ToArray()
        .Select(node => node ?? stack.Pop())
        .Take(..^emptyNodes)
        .Select((value, index) => index * (long)value)
        .Sum();

    Console.WriteLine($"Answer part one: {answer}");
}

void PartTwo()
{
    var answer = 0L;

    var emptyNodes = 0;
    var stack = new Stack<int>();

    answer = File
        .ReadAllText("input.txt")
        .SelectMany((c, index) =>
        {
            var fileId = index / 2;

            return Enumerable
                .Range(0, int.Parse($"{c}"))
                .Select(x =>
                {
                    if (index % 2 == 0)
                    {
                        stack.Push(fileId);
                        return fileId;
                    }

                    emptyNodes++;
                    return (int?)null;
                });

        })
        .ToArray()
        .Select(node =>
        {
            return node ?? stack.Pop();
        })
        .Take(..^emptyNodes)
        .Select((value, index) => index * (long)value)
        .Sum();

    Console.WriteLine($"Answer part two: {answer}");
}

PartOne();
PartTwo();

Console.ReadLine();
