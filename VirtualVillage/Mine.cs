namespace VirtualVillage;

public class Mine : Entity
{
    public int OreRemaining { get; }

    private readonly List<GoapAction> actions = [];

    public Mine(Location location, int oreRemaining) : base("Mine", location)
    {
        OreRemaining = oreRemaining;

        actions.Add(
            new GoapAction.Builder("Mine ore", 5)
            .WithPrecondition(s =>
                s.Get<Location>("agent_location").DistanceTo(Location) == 0 &&
                s.Get<int>("agent_pickaxe") > 0 &&
                s.Get<int>(GetStateKey("ore")) > 0)
            .WithEffect(s =>
                {
                    s.Inc("agent_ore", 1);
                    s.Dec(GetStateKey("ore"), 1);
                })
            .WithEntity(this)
            .WithTag("Miner")
            .Build());
    }

    public override void Update(WorldState state)
    {
        state[GetStateKey("ore")] = OreRemaining;
    }

    public override IEnumerable<GoapAction> GetProvidedActions() => actions;
}
