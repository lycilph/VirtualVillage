namespace VirtualVillage;

public readonly record struct Location(int X, int Y)
{
    public int DistanceTo(Location other)
        => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
}