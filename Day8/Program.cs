var input = File.ReadAllLines("input.txt");
var map = input.Select(line => line.ToArray()).ToArray();
Dictionary<char, IList<Point>> antennas = [];
HashSet<Point> pointSet = [];

const char emptySpace = '.';
const char marker = '#';

for (var y = 0; y < map.Length; y++)
{
    for (var x = 0; x < map[y].Length; x++)
    {
        var terrain = map[y][x];
        if (terrain != emptySpace)
        {
            if (!antennas.ContainsKey(terrain))
            {
                antennas[terrain] = [];
            }

            antennas[terrain].Add(new Point(x, y));
        }
    }
}

void PrintMap(char[][] map)
{
    for (var y = 0; y < map.Length; y++)
    {
        for (var x = 0; x < map[y].Length; x++)
        {
            var consoleColor = Console.ForegroundColor;

            if (map[y][x] != emptySpace)
            {
                if (map[y][x] == marker)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
            }

            Console.Write(map[y][x]);

            Console.ForegroundColor = consoleColor;
        }

        Console.WriteLine();
    }
}

bool MarkAntinodeOnMap(Point point, char[][] map)
{
    if (point.Y >= 0 && point.Y < map.Length &&
        point.X >= 0 && point.X < map[point.Y].Length)
    {
        pointSet.Add(point);
        map[point.Y][point.X] = marker;
        return true;
    }

    return false;
}

int CountAntinodesPartTwo(char[][] map)
{
    return map.Sum(x => x.Count(y =>
    {
        if (y == emptySpace)
        {
            return false;
        }

        if (antennas.TryGetValue(y, out var points))
        {
            if (points.Count <= 1)
            {
                return false;
            }
        }

        return true;
    }));
}

void MapAntinodes(Point point, Point[] antennas, char[][] map)
{
    foreach (var antenna in antennas)
    {
        var antiNode = antenna + (antenna - point);
        MarkAntinodeOnMap(antiNode, map);
    }
}

void MapAntinodesPartTwo(Point point, Point[] antennas, char[][] map)
{
    foreach (var antenna in antennas)
    {
        var distance = antenna - point;

        var antiNode = antenna + distance;
        var marked = MarkAntinodeOnMap(antiNode, map);

        while (marked)
        {
            antiNode = antiNode + distance;
            marked = MarkAntinodeOnMap(antiNode, map);
        }
    }
}

void PartOne()
{
    var answer = 0;

    foreach (var antenna in antennas)
    {
        foreach (var point in antenna.Value)
        {
            MapAntinodes(point, antenna.Value.Where(x => x != point).ToArray(), map);
        }
    }

    answer = pointSet.Count;

    Console.WriteLine($"Answer part one: {answer}");
}

char[][] CopyMap(char[][] map)
{
    char[][] mapCopy = new char[map.Length][];

    for (var y = 0; y < map.Length; y++)
    {
        mapCopy[y] = new char[map[y].Length];

        for (var x = 0; x < map.Length; x++)
        {
            mapCopy[y][x] = map[y][x];
        }
    }

    return mapCopy;
}

void PartTwo()
{
    var answer = 0;

    foreach (var antenna in antennas)
    {
        var mapCopy = CopyMap(map);

        foreach (var point in antenna.Value)
        {
            MapAntinodesPartTwo(point, antenna.Value.Where(x => x != point).ToArray(), map);
        }
    }

    answer = pointSet.Count + antennas.Where(x => x.Value.Count > 1).Sum(x => x.Value.Count - 1);

    var bla = CountAntinodesPartTwo(map);

    Console.WriteLine($"Answer part two: {answer}");
}

PartOne();
PartTwo();

Console.ReadLine();

record Point(int X, int Y)
{
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
}
