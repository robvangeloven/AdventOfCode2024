
void PartOne()
{
    var emptyNodes = 0;
    var stack = new Stack<int>();

    var answer = File
        .ReadAllText("input.txt")
        .SelectMany((c, index) =>
        {
            var fileId = index / 2;

            var result = Enumerable
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

            return result;

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
    var answer = 0;

    Console.WriteLine($"Answer part two: {answer}");
}

PartOne();
PartTwo();

Console.ReadLine();
