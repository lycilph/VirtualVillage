namespace VirtualVillage;

public struct Position(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;

    // Manhattan distance is usually best for grid-based village logic
    public int DistanceTo(Position other) => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

    // Static helper for a "Zero" position
    public static Position Zero => new(0, 0);

    public override string ToString() => $"({X}, {Y})";
}