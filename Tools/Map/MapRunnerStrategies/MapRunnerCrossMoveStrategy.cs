namespace Tools.Map.MapRunnerStrategies;

using System.Collections.Generic;

public class MapRunnerCrossMoveStrategy<T> : IMapRunnerMoveStrategy<T>
{
    public IEnumerable<Tile<T>> GetNextPossibleMoves(int x, int y, Map<T> map)
    {
        var result = new List<Tile<T>>(4);

        if (map.TryGetValue(x - 1, y, out var tile))
        {
            result.Add(tile);
        }

        if (map.TryGetValue(x + 1, y, out tile))
        {
            result.Add(tile);
        }

        if (map.TryGetValue(x, y - 1, out tile))
        {
            result.Add(tile);
        }

        if (map.TryGetValue(x, y + 1, out tile))
        {
            result.Add(tile);
        }

        return result;
    }
}
