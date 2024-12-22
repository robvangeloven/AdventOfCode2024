namespace Tools.Map;

public abstract record MapRunner<T>
{
    private readonly Map<T> _map;
    private readonly IMapRunnerMoveStrategy<T> _mapRunnerMoveStrategy;

    public int X { get; private set; }

    public int Y { get; private set; }

    public MapRunner(
        (int x, int y) startingPosition,
        IMapRunnerMoveStrategy<T> mapRunnerMoveStrategy,
        Map<T> map)
    {
        X = startingPosition.x;
        Y = startingPosition.y;
        _mapRunnerMoveStrategy = mapRunnerMoveStrategy ?? throw new ArgumentNullException(nameof(mapRunnerMoveStrategy));
        _map = map ?? throw new ArgumentNullException(nameof(map));

        if (_map.TryGetValue(X, Y, out var startingTile))
        {
            startingTile.Visit();
        }
    }

    public bool MoveNext()
    {
        var possibleMoves = _mapRunnerMoveStrategy.GetNextPossibleMoves(X, Y, _map);

        var nextMove = DecideNextMove(possibleMoves);

        if (nextMove is not null)
        {
            X = nextMove.X;
            Y = nextMove.Y;

            nextMove.Visit();
        }

        return nextMove != null;
    }

    public abstract Tile<T>? DecideNextMove(IEnumerable<Tile<T>> possibleMoves);
}
