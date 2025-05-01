using System.Numerics;

namespace VirtualVillage.Objects;

public class Storehouse : IWorldObject
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public string Name { get; set; } = "Storehouse";

    public Dictionary<string, int> Inventory { get; set; } = [];

    public bool Has(string name) => Inventory.TryGetValue(name, out var value) && value > 0;

    public void Update(World world)
    {
    }

    public void Render()
    {
        Console.WriteLine($"{Name}:");
        foreach (var item in Inventory)
            Console.WriteLine($"  {item}");
    }
}
