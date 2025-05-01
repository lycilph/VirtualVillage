using System.Numerics;

namespace VirtualVillage.Objects;

public class Tree(Vector2 position) : IWorldObject
{
    public Vector2 Position { get; set; } = position;
    public string Name { get; set; } = "Tree";
    public int Health { get; set; } = 3;

    public void Update(World world)
    {
    }

    public void Render()
    {
        Console.WriteLine($"{Name} - {Position} - Health {Health}");
    }
}
