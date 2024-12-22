using System.Text.RegularExpressions;

(double X, double Y)? Calculate(Button a, Button b, Button c)
{
    var determinant = (a.X * b.Y) - (b.X * a.Y);

    if (determinant == 0)
    {
        return null;
    }

    var x = ((c.X * b.Y) - (b.X * c.Y)) / (double)determinant;
    var y = ((a.X * c.Y) - (c.X * a.Y)) / (double)determinant;

    return (x, y);
}

void PartOne()
{
    var answer = File
     .ReadAllLines("input.txt")
     .Chunk(4)
     .Select(line =>
     {
         var match = CoordinateValuesMatch().Match(line[0]);
         var buttonA = new Button { X = int.Parse(match.Groups["X"].ValueSpan), Y = int.Parse(match.Groups["Y"].ValueSpan) };

         match = CoordinateValuesMatch().Match(line[1]);
         var buttonB = new Button { X = int.Parse(match.Groups["X"].ValueSpan), Y = int.Parse(match.Groups["Y"].ValueSpan) };

         match = CoordinateValuesMatch().Match(line[2]);
         var prize = new Button { X = int.Parse(match.Groups["X"].ValueSpan), Y = int.Parse(match.Groups["Y"].ValueSpan) };

         var buttonPresses = Calculate(buttonA, buttonB, prize);

         // Check if range is between 0 and 100
         // Check if it's a whole "press" of a button:
         if (buttonPresses?.X is < 0 or > 100
             || buttonPresses?.X % 1 != 0
             || buttonPresses?.Y is < 0 or > 100
             || buttonPresses?.Y % 1 != 0)
         {
             return 0;
         }

         return buttonPresses?.X * 3 + buttonPresses?.Y;
     })
     .Sum();

    Console.WriteLine($"Answer part one: {answer}");
}

void PartTwo()
{
    var offset = 10000000000000;

    var answer = File
     .ReadAllLines("input.txt")
     .Chunk(4)
     .Select(line =>
     {
         var match = CoordinateValuesMatch().Match(line[0]);
         var buttonA = new Button { X = int.Parse(match.Groups["X"].ValueSpan), Y = int.Parse(match.Groups["Y"].ValueSpan) };

         match = CoordinateValuesMatch().Match(line[1]);
         var buttonB = new Button { X = int.Parse(match.Groups["X"].ValueSpan), Y = int.Parse(match.Groups["Y"].ValueSpan) };

         match = CoordinateValuesMatch().Match(line[2]);
         var prize = new Button { X = offset + int.Parse(match.Groups["X"].ValueSpan), Y = offset + int.Parse(match.Groups["Y"].ValueSpan) };

         var buttonPresses = Calculate(buttonA, buttonB, prize);

         // Check if it's a whole "press" of a button:
         if (buttonPresses?.X is < 0
             || buttonPresses?.X % 1 != 0
             || buttonPresses?.Y is < 0
             || buttonPresses?.Y % 1 != 0)
         {
             return 0;
         }

         return buttonPresses?.X * 3 + buttonPresses?.Y;
     })
     .Sum();

    Console.WriteLine($"Answer part two: {answer}");
}

PartOne();
PartTwo();

Console.ReadLine();

internal record Button
{
    public Button()
    {

    }

    public Button(long x, long y)
    {
        X = x;
        Y = y;
    }

    public long X { get; init; }

    public long Y { get; init; }
}

partial class Program
{
    [GeneratedRegex(@"\D+(?<X>\d*)\D+(?<Y>\d*)")]
    private static partial Regex CoordinateValuesMatch();
}
