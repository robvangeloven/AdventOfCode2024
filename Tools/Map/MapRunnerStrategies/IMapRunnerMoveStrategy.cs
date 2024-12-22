namespace Tools.Map;

public interface IMapRunnerMoveStrategy<T>
{
    IEnumerable<Tile<T>> GetNextPossibleMoves(int x, int y, Map<T> map);
}
