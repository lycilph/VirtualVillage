namespace VirtualVillage;

public class World
{
    public int Tick { get; private set; }
    public List<Villager> Villagers { get; } = [];
    public Storehouse Storehouse { get; set; } = null!;

    public void Step()
    {
        Tick++;
        foreach (var v in Villagers)
            v.Update(this);
    }
}