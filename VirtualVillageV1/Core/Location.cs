namespace VirtualVillage.Core;

public readonly record struct Location(int X, int Y)
{
    private static readonly Random rnd = new(DateTime.Now.Millisecond);

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

    public static Location Random()
    {
        var x = rnd.Next(-5, 6); // Should give a number between -5 and 5 (inclusive)
        var y = rnd.Next(-5, 6); // Should give a number between -5 and 5 (inclusive)
        return new Location(x, y);
    }
}