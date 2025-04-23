namespace VirtualVillageConsoleApp;

public readonly struct Position(double x, double y)
{
    public double X { get; } = x;
    public double Y { get; } = y;

    public override string ToString() => $"({X}, {Y})";
}
