namespace VirtualVillage;

public class Location
{
    public int X = 0;
    public int Y = 0;

    public Location Clone() => new() { X = X, Y = Y };

    public override string ToString() => $"({X},{Y})";
}
