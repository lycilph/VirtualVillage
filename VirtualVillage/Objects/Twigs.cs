using System.Numerics;

namespace VirtualVillage.Objects;

public class Twigs(Vector2 position) : IWorldObject
{
    public Vector2 Position { get; set; } = position;
    public string Name { get; set; } = "Twig";

    public void Update(World world)
    {
    }

    public void Render()
    {
        Console.WriteLine($"{Name} - {Position}");
    }
}
