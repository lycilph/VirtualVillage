using VirtualVillage.Actions;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Planning;

namespace VirtualVillage.Entities;

public class Storehouse : WorldObject<Storehouse>, IEntity
{
    public int Axes { get; set;  }
    public int Pickaxes { get; set;  }
    public int Wood { get; set; }
    public int Ore { get; set; }
    public Dictionary<string, int> Inventory { get; } = [];

    private readonly List<GoapAction> actions = [];

    public Storehouse(Location location) : base("Storehouse", location)
    {
        foreach (var item in Keys.Items)
        {
            actions.Add(new DepositAction(item, 1, this));
            actions.Add(new PickupAction(item, 1, this));
        }
    }

    public override void Update(WorldState state) 
    {
        state[GetStateKey(Keys.Axe)] = Axes;
        state[GetStateKey(Keys.Pickaxe)] = Pickaxes;
        state[GetStateKey(Keys.Wood)] = Wood;
        state[GetStateKey(Keys.Ore)] = Ore;
    }

    public override void Render() => Console.WriteLine($"Storehouse (Axes: {Axes}, Pickaxes: {Pickaxes}, Wood: {Wood}, Ore: {Ore})");

    public IEnumerable<GoapAction> GetProvidedActions() => actions;
}
