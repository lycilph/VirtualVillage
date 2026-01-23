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

    private readonly List<GoapAction> actions = [];

    public Storehouse(Location location) : base("Storehouse", location)
    {
        actions.Add(new DepositAction(Keys.Wood, 1, this));
        actions.Add(new DepositAction(Keys.Ore, 1, this));

        actions.Add(new DepositAction(Keys.Axe, 1, this));
        actions.Add(new DepositAction(Keys.Pickaxe, 1, this));

        actions.Add(new PickupAction(Keys.Wood, 1, this));
        actions.Add(new PickupAction(Keys.Ore, 1, this));

        actions.Add(new PickupAction(Keys.Axe, 1, this));
        actions.Add(new PickupAction(Keys.Pickaxe, 1, this));
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
