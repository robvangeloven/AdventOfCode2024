var reports = (await File.ReadAllLinesAsync("example.txt"))
    .Select(line => line.Split(' ').Select(int.Parse).ToList())
    .ToList();

bool ScanReport(IList<int> report)
{
    var deltas = report.Zip(report.Skip(1), (a, b) => b - a);

    return deltas.All(delta => delta is >= 1 and <= 3) ||
        deltas.All(delta => delta is <= -1 and >= -3);
}

void PartOne()
{
    var answer = reports.Where(ScanReport).Count();

    Console.WriteLine($"Answer part one: {answer}");
}

void PartTwo()
{
    var answer = reports
        .Where(report =>
        {
            var safeReport = ScanReport(report);

            if (safeReport)
            {
                return true;
            }

            for (int i = 0; i < report.Count; i++)
            {
                var repo = new List<int>(report);
                repo.RemoveAt(i);

                if (ScanReport(repo))
                {
                    return true;
                }
            }

            return false;
        });

    Console.WriteLine($"Answer part two: {answer.Count()}");
}

PartOne();
PartTwo();

Console.ReadLine();