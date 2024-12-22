namespace Tools.Map;

public record Tile<T>
{
    public required int X { get; init; }

    public required int Y { get; init; }

    public required T Value { get; set; }

    public int VisitCount { get; private set; }

    public bool Visited => VisitCount > 0;

    public void Visit()
    {
        VisitCount++;
    }
}
