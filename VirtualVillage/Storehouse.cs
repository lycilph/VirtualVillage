namespace VirtualVillage;

public class Storehouse
{
    public Position Position { get; }

    public int Wood { get; private set; }
    public int Ore { get; private set; }

    public Storehouse(Position position)
    {
        Position = position;
    }

    public void AddWood(int amount)
    {
        Wood += amount;
        Console.WriteLine($"[Storehouse] Wood +{amount} -> {Wood}");
    }

    public void AddOre(int amount)
    {
        Ore += amount;
        Console.WriteLine($"[Storehouse] Ore +{amount} -> {Ore}");
    }
}
