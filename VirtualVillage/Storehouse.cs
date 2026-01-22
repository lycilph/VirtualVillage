
namespace VirtualVillage;

public class Storehouse : Entity
{
    public int Axes { get; set;  }
    public int Pickaxes { get; set;  }
    public int Wood { get; set; }
    public int Ore { get; set; }


    private readonly List<GoapAction> actions = [];

    public Storehouse(Location location) : base("Storehouse", location)
    {
        actions.Add(
            new GoapAction.Builder("Pickup Axe", 1)
            .WithPrecondition(s =>
                s.Get<Location>("agent_location").DistanceTo(Location) == 0 &&
                s.Get<int>(GetStateKey("axe")) > 0)
            .WithEffect(s =>
            {
                s.Inc("agent_axe", 1);
                s.Dec(GetStateKey("axe"), 1);
            })
            .WithEntity(this)
            .WithTag("All")
            .Build());

        actions.Add(
            new GoapAction.Builder("Pickup Pickaxe", 1)
            .WithPrecondition(s =>
                s.Get<Location>("agent_location").DistanceTo(Location) == 0 &&
                s.Get<int>(GetStateKey("pickaxe")) > 0)
            .WithEffect(s =>
            {
                s.Inc("agent_pickaxe", 1);
                s.Dec(GetStateKey("pickaxe"), 1);
            })
            .WithEntity(this)
            .WithTag("All")
            .Build());

        actions.Add(
            new GoapAction.Builder("Deposit Axe", 1)
            .WithPrecondition(s =>
                s.Get<Location>("agent_location").DistanceTo(Location) == 0 &&
                s.Get<int>("agent_axe") > 0)
            .WithEffect(s =>
            {
                s.Inc(GetStateKey("axe"), 1);
                s.Dec("agent_axe", 1);
            })
            .WithEntity(this)
            .WithTag("All")
            .Build());

        actions.Add(
            new GoapAction.Builder("Deposit Wood", 1)
            .WithPrecondition(s =>
                s.Get<Location>("agent_location").DistanceTo(Location) == 0 &&
                s.Get<int>("agent_wood") > 0)
            .WithEffect(s =>
            {
                s.Dec("agent_wood", 1);
                s.Inc(GetStateKey("wood"), 1);
            })
            .WithEntity(this)
            .WithTag("All")
            .Build());

        actions.Add(
            new GoapAction.Builder("Deposit Ore", 1)
            .WithPrecondition(s =>
                s.Get<Location>("agent_location").DistanceTo(Location) == 0 &&
                s.Get<int>("agent_ore") > 0)
            .WithEffect(s =>
            {
                s.Dec("agent_ore", 1);
                s.Inc(GetStateKey("ore"), 1);
            })
            .WithEntity(this)
            .WithTag("All")
            .Build());
    }

    public override void Update(WorldState state) 
    {
        state[GetStateKey("axe")] = Axes;
        state[GetStateKey("pickaxe")] = Pickaxes;
        state[GetStateKey("wood")] = Wood;
        state[GetStateKey("ore")] = Ore;
    }

    public override IEnumerable<GoapAction> GetProvidedActions() => actions;
}
