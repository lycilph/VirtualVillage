using System.Numerics;

namespace VirtualVillage.Objects;

public interface IWorldObject
{
    public Vector2 Position { get; set; }
    public string Name { get; set; }

    public void Update(World world);
    public void Render();
}
