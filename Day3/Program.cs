using System.Text.RegularExpressions;

var input = await File.ReadAllTextAsync("input.txt");

void PartOne()
{
    var matches = Regex.Matches(input, @"mul\((?<value>\d{1,3},\d{1,3})\)");

    var answer = matches.Select(match =>
    {
        var values = match.Groups["value"].Value.Split(',').Select(int.Parse).ToList();
        return values[0] * values[1];
    }).Sum();

    Console.WriteLine($"Answer part one: {answer}");
}

void PartTwo()
{
    var matches = Regex.Matches(input, @"\A(?<value>.*?)(?=don't)|(?<=do\(\))(?<value>.*?)(?=don't)|(?<=do\(\))(?<value>.*?)$", RegexOptions.Singleline);

    var answer = matches.SelectMany(x =>
    {
        return Regex
        .Matches(x.Groups["value"].Value, @"mul\((\d{1,3},\d{1,3})\)")
        .Select(match =>
        {
            var values = match.Groups[1].Value.Split(',').Select(int.Parse).ToList();
            return values[0] * values[1];
        });
    }).Sum();

    Console.WriteLine($"Answer part two: {answer}");
}

PartOne();
PartTwo();

Console.ReadLine();
