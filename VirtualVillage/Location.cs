namespace VirtualVillage;

public class Location
{
    public required string Name;
    public int X = 0;
    public int Y = 0;

    public Location Clone() => new() { Name = Name, X = X, Y = Y };

    public override string ToString() => $"{Name} ({X},{Y})";
}
