using VirtualVillage.Entities;

namespace VirtualVillage;

public class World
{
    public int Tick { get; private set; }
    public List<Villager> Villagers { get; } = [];
    public List<WorldEntity> Entities { get; } = [];

    public void Step()
    {
        Tick++;
        foreach (var v in Villagers)
            v.Update(this);
    }
}