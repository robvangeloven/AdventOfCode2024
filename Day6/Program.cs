(int GuardX, int GuardY, char[][] Map) Load()
{
    var input = File.ReadAllLines("example.txt");
    var map = input.Select(line => line.ToArray()).ToArray();

    int guardX = 0, guardY = 0;

    var guardFound = false;

    for (var y = 0; y < map.Length; y++)
    {
        for (var x = 0; x < map[y].Length; x++)
        {
            if (map[y][x] == '^')
            {
                guardX = x;
                guardY = y;
                guardFound = true;
                break;
            }
        }

        if (guardFound)
        {
            break;
        }
    }

    return (guardX, guardY, map);
}

void PartOne()
{
    var (guardX, guardY, map) = Load();

    var answer = 0;
    var guard = new Guard(guardY, guardX, map);

    while (guard.Move())
    {
    }

    answer = guard.StepCounter;

    Console.WriteLine($"Answer part one: {answer}");
}

void PartTwo()
{
    var (guardX, guardY, map) = Load();

    var answer = 0;
    var guard = new Guard(guardY, guardX, map);

    while (guard.Move())
    {
    }

    answer = guard.ShenanigansCounter;

    guard.PrintMap();

    Console.WriteLine($"Answer part two: {answer}");
}

PartOne();
PartTwo();

Console.ReadLine();

public enum Direction
{
    North,
    East,
    South,
    West
}

public class Guard
{
    private const char _stopSign = '!';
    private const char _obstacle = '#';
    private const char _untroddenPath = '.';
    private const char _shenanigans = 'O';

    private readonly char[][] _map;

    public int PositionY { get; private set; }

    public int PositionX { get; private set; }

    public int StepCounter { get; private set; } = 1;

    public int ShenanigansCounter { get; private set; }

    public Direction Direction { get; private set; }

    public Guard(int startY, int startX, char[][] map)
    {
        PositionY = startY;
        PositionX = startX;
        _map = map ?? throw new ArgumentNullException(nameof(map));

        _map[PositionY][PositionX] = _untroddenPath;
    }

    private char GetNextStepTerrain(Direction? direction = null, int? x = null, int? y = null)
    {
        direction ??= Direction;

        (var nextX, var nextY) = GetNextStepPosition(direction.Value, x, y);

        switch (Direction)
        {
            case Direction.North:
                if (nextY < 0)
                {
                    return _stopSign;
                }

                break;

            case Direction.East:
                if (nextX > _map[PositionY].Length)
                {
                    return _stopSign;
                }

                break;

            case Direction.South:
                if (nextY > _map.Length - 1)
                {
                    return _stopSign;
                }

                break;

            case Direction.West:
                if (nextX < 0)
                {
                    return _stopSign;
                }

                break;
        }

        return _map[nextY][nextX];
    }

    private static Direction Rotate(Direction direction) => (Direction)(((int)direction + 1) % 4);

    private (int x, int y) GetNextStepPosition(Direction? direction = null, int? x = null, int? y = null)
    {
        direction ??= Direction;

        x ??= PositionX;
        y ??= PositionY;

        switch (direction)
        {
            case Direction.North:
                y--;
                break;

            case Direction.East:
                x++;
                break;

            case Direction.South:
                y++;
                break;

            case Direction.West:
                x--;
                break;
        }

        return (x.Value, y.Value);
    }

    public void PrintMap()
    {
        for (var y = 0; y < _map.Length; y++)
        {
            for (var x = 0; x < _map[y].Length; x++)
            {
                var consoleColor = Console.ForegroundColor;

                if (_map[y][x] == _shenanigans)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                if (x == PositionX && y == PositionY)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.Write($"{_map[y][x]}");

                Console.ForegroundColor = consoleColor;
            }

            Console.WriteLine();
        }
    }

    public bool DetectLoop(Direction direction, int? x = null, int? y = null, int depth = 0)
    {
        if (depth > 100)
        {
            return false;
        }

        var nextTerrain = GetNextStepTerrain(direction, x, y);

        if (nextTerrain == direction.ToString()[0])
        {
            return true;
        }

        switch (nextTerrain)
        {
            case _obstacle:
                direction = Rotate(direction);
                (x, y) = GetNextStepPosition();
                return DetectLoop(direction, x, y, ++depth);

            case _stopSign:
                return false;

            default:
                direction = Rotate(direction);
                (x, y) = GetNextStepPosition();
                return DetectLoop(direction, x, y, ++depth);
        }
    }

    public bool Move()
    {
        var nextTerrain = GetNextStepTerrain();

        switch (nextTerrain)
        {
            case _obstacle:
                Direction = Rotate(Direction);
                return Move();

            case _stopSign:
                return false;

            default:
                if (_map[PositionY][PositionX] is _untroddenPath or _shenanigans)
                {
                    StepCounter++;

                    if (_map[PositionY][PositionX] is _untroddenPath)
                    {
                        _map[PositionY][PositionX] = Direction.ToString()[0];
                    }
                }

                if (DetectLoop(Rotate(Direction)))
                {
                    (var x, var y) = GetNextStepPosition();
                    _map[y][x] = _shenanigans;

                    ShenanigansCounter++;
                }

                (PositionX, PositionY) = GetNextStepPosition();

                return true;
        }
    }
}
