var listOne = new List<int>();
var listTwo = new List<int>();

var lines = await File.ReadAllLinesAsync("input.txt");

foreach (var line in lines)
{
    var values = line.Split("  ");

    listOne.Add(int.Parse(values[0]));
    listTwo.Add(int.Parse(values[1]));
}

void PartOne()
{

    listOne.Sort();
    listTwo.Sort();

    var answer = 0;

    for (var i = 0; i < listOne.Count; i++)
    {
        answer += Math.Abs(listOne[i] - listTwo[i]);
    }

    Console.WriteLine($"Answer part one: {answer}");
}

void PartTwo()
{
    var answer = 0;

    foreach (var item in listOne)
    {
        var count = listTwo.FindAll(x => x == item).Count;

        answer += item * count;
    }

    Console.WriteLine($"Answer part two: {answer}");
}

PartOne();
PartTwo();

Console.ReadLine();