namespace VirtualVillage;

public class Mine : Entity
{
    private readonly string ore_key;

    public int OreRemaining { get; }

    private readonly List<GoapAction> actions = [];

    public Mine(Location location, int oreRemaining) : base("Mine", location)
    {
        OreRemaining = oreRemaining;

        ore_key = GetStateKey("ore");

        var chop = new GoapAction.Builder("Mine ore", 5)
            .WithPrecondition(s =>
                s.Get<Location>("agent_location").DistanceTo(Location) == 0 &&
                s.Get<int>("agent_pickaxe") > 0 &&
                s.Get<int>(ore_key) > 0)
            .WithEffect(s =>
            {
                s.Inc("agent_ore", 1);
                s.Dec(ore_key, 1);
            })
            .WithEntity(this)
            .Build();
        actions.Add(chop);
    }

    public override void Update(WorldState state)
    {
        state[ore_key] = OreRemaining;
    }

    public override IEnumerable<GoapAction> GetProvidedActions() => actions;
}
