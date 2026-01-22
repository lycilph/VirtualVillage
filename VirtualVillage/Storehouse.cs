
namespace VirtualVillage;

public class Storehouse : Entity
{
    public int StoredAxes { get; set;  }
    public int StoredPickaxes { get; set;  }
    public int StoredWood { get; set; }
    public int StoredOre { get; set; }


    private readonly List<GoapAction> actions = [];

    public Storehouse(Location location) : base("Storehouse", location)
    {
        actions.Add(
            new GoapAction.Builder("Pickup Axe", 1)
            .WithPrecondition(s =>
                s.Get<Location>("agent_location").DistanceTo(Location) == 0 &&
                s.Get<int>(GetStateKey("stored_axes")) > 0)
            .WithEffect(s =>
            {
                s.Inc("agent_axe", 1);
                s.Dec(GetStateKey("stored_axes"), 1);
            })
            .WithEntity(this)
            .Build());

        actions.Add(
            new GoapAction.Builder("Pickup Pickaxe", 1)
            .WithPrecondition(s =>
                s.Get<Location>("agent_location").DistanceTo(Location) == 0 &&
                s.Get<int>(GetStateKey("stored_pickaxes")) > 0)
            .WithEffect(s =>
            {
                s.Inc("agent_pickaxe", 1);
                s.Dec(GetStateKey("stored_pickaxes"), 1);
            })
            .WithEntity(this)
            .Build());

        actions.Add(
            new GoapAction.Builder("Deposit Axe", 1)
            .WithPrecondition(s =>
                s.Get<Location>("agent_location").DistanceTo(Location) == 0 &&
                s.Get<int>("agent_axe") > 0)
            .WithEffect(s =>
            {
                s.Inc("stored_axes", 1);
                s.Dec("agent_axe", 1);
            })
            .WithEntity(this)
            .Build());

        actions.Add(
            new GoapAction.Builder("Deposit Wood", 1)
            .WithPrecondition(s =>
                s.Get<Location>("agent_location").DistanceTo(Location) == 0 &&
                s.Get<int>("agent_wood") > 0)
            .WithEffect(s =>
            {
                s.Dec("agent_wood", 1);
                s.Inc("stored_wood", 1);
            })
            .WithEntity(this)
            .Build());

        actions.Add(
            new GoapAction.Builder("Deposit Ore", 1)
            .WithPrecondition(s =>
                s.Get<Location>("agent_location").DistanceTo(Location) == 0 &&
                s.Get<int>("agent_ore") > 0)
            .WithEffect(s =>
            {
                s.Dec("agent_ore", 1);
                s.Inc("stored_ore", 1);
            })
            .WithEntity(this)
            .Build());
    }

    public override void Update(WorldState state) 
    {
        state[GetStateKey("stored_axes")] = StoredAxes;
        state[GetStateKey("stored_pickaxes")] = StoredPickaxes;
        state[GetStateKey("stored_wood")] = StoredWood;
        state[GetStateKey("stored_ore")] = StoredOre;
    }

    public override IEnumerable<GoapAction> GetProvidedActions() => actions;
}
