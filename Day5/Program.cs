var input = File.ReadAllLines("input.txt");
var rules = new Dictionary<int, List<int>>();
List<List<int>> manuals = [];

var instructionProcessing = true;

foreach (var line in input)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        instructionProcessing = false;
        continue;
    }

    if (instructionProcessing)
    {
        var values = line.Split('|').Select(int.Parse).ToArray();

        if (!rules.ContainsKey(values[0]))
        {
            rules[values[0]] = [];
        }

        rules[values[0]].Add(values[1]);
    }
    else
    {
        manuals.Add([.. line.Split(',').Select(int.Parse)]);
    }
}

bool IsValidPage(IList<int> pages)
{
    return rules.All(ruleSet =>
    {
        var ruleIndex = pages.IndexOf(ruleSet.Key);

        if (ruleIndex < 0)
        {
            return true;
        }

        return ruleSet.Value.All(rule =>
        {
            var pageIndex = pages.IndexOf(rule);

            return pageIndex < 0
            ? true
            : ruleIndex < pageIndex;
        });
    });
}

void PartOne()
{
    var answer = 0;

    answer = manuals.Sum(manual => IsValidPage(manual) ? manual[(manual.Count - 1) / 2] : 0);

    Console.WriteLine($"Answer part one: {answer}");
}

void PartTwo()
{
    var answer = 0;

    answer = manuals.Sum(manual =>
    {
        if (IsValidPage(manual))
        {
            return 0;
        }

        manual.Sort((pageA, pageB) =>
        {
            if (!rules.TryGetValue(pageA, out var pageARules))
            {
                return 1;
            }

            if (!rules.TryGetValue(pageB, out var pageBRules))
            {
                return -1;
            }

            return pageARules.Contains(pageB)
            ? -1
            : 1;
        });

        return manual[(manual.Count - 1) / 2];
    });

    Console.WriteLine($"Answer part two: {answer}");
}

PartOne();
PartTwo();

Console.ReadLine();
