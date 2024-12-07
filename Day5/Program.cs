//var input = await File.ReadAllLinesAsync("input.txt");

var input = File.OpenText("input.txt");
string? line;
var instructions = new Dictionary<int, List<int>>();

do
{
    line = input.ReadLine();

    if (line is null)
    {
        continue;
    }

    var values = line.Split('|').Select(int.Parse).ToList();

    if (!instructions.ContainsKey(values[0]))
    {
        instructions.Add(values[0], []);
    }

    instructions[values[0]].Add(values[1]);

} while (!string.IsNullOrWhiteSpace(line));

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
