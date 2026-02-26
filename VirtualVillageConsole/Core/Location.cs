namespace VirtualVillageConsole.Core;

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

    public int DistanceTo(Location other) => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

    public override string ToString() => $"({X},{Y})";

    public static Location Random(int min = -10, int max = 10)
    {
        var x = rnd.Next(min, max+1); // Should give a number between min and max (inclusive)
        var y = rnd.Next(min, max+1); // Should give a number between min and max (inclusive)
        return new Location(x, y);
    }
}