namespace VirtualVillage.Core;

public readonly record struct Location(int X, int Y)
{
    public Location StepTowards(Location target)
    {
        int dx = Math.Sign(target.X - X);
        int dy = Math.Sign(target.Y - Y);

        // Manhattan movement: one axis per step
        if (dx != 0)
            return this with { X = X + dx };

        return this with { Y = Y + dy };
    }

    public int DistanceTo(Location other)
        => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

    public override string ToString() => $"({X},{Y})";
}