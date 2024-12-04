var input = await File.ReadAllLinesAsync("input.txt");
var matrix = input.Select(line => line.ToArray()).ToArray();

void PartOne()
{
    var answer = 0;

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
