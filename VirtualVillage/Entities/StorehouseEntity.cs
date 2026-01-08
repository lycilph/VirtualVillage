using VirtualVillage.Actions;

namespace VirtualVillage.Entities;

public class StorehouseEntity : WorldEntity
{
    public int Wood { get; private set; }
    public int Ore { get; private set; }

    public StorehouseEntity(Position position) : base(position) {}

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

    public override IEnumerable<GoapAction> GetActionsFor(Villager villager, World world)
    {
        yield return new MoveAction(Position) { Source = this };
        yield return new DepositWoodAction(Position) { Source = this };
        yield return new DepositOreAction(Position) { Source = this };
    }
}
