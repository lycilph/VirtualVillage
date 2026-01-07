namespace VirtualVillage;

public readonly record struct Position(int X, int Y)
{
    public int DistanceTo(Position other)
        => Math.Abs(X - other.X) + Math.Abs(Y - other.Y); // Manhattan for now
}
