using System.Numerics;

namespace VirtualVillage.Objects;

public class Storehouse : IWorldObject
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public string Name { get; set; } = "Storehouse";

    public Dictionary<string, int> Inventory { get; set; } = [];

    public void Update(World world)
    {
    }

    public void Render()
    {
        Console.WriteLine($"{Name} - {Position}:");
        foreach (var item in Inventory)
            Console.WriteLine($"  {item}");
    }
}
