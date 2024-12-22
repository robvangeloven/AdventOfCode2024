namespace Tools.Map;

public record Map<T>
{
    private Tile<T>[][] _map = [];

    public bool IsInBounds(int x, int y) => x >= 0 && y >= 0 && y < _map.Length && x < _map[y].Length;

    public Tile<T> this[int x, int y]
    {
        get => _map[y][x];
        set => _map[y][x] = value;
    }

    public bool TryGetValue(int x, int y, out Tile<T> result)
    {
        if (IsInBounds(x, y))
        {
            result = this[x, y];

            return true;
        }

        result = default!;

        return false;
    }
}
