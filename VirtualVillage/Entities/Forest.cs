namespace VirtualVillage.Entities;

public class Forest : Entity
{
    public int WoodRemaining { get; }

    private readonly List<GoapAction> actions = [];

    public Forest(Location location, int woodRemaining) : base("Forest", location)
    {
        WoodRemaining = woodRemaining;

        actions.Add(
            new GoapAction.Builder("Chop Wood", 5)
            .WithPrecondition(s => 
                s.Get<Location>("agent_location").DistanceTo(Location) == 0 &&
                s.Get<int>("agent_axe") > 0 &&
                s.Get<int>(GetStateKey("wood")) > 0)
            .WithEffect(s => 
                {
                    s.Inc("agent_wood", 1);
                    s.Dec(GetStateKey("wood"), 1);
                })
            .WithEntity(this)
            .WithTag("Lumberjack")
            .Build());
    }

    public override void Update(WorldState state)
    {
        state[GetStateKey("wood")] = WoodRemaining;
    }

    public override void Render() => Console.WriteLine($"Forest (remaining wood: {WoodRemaining})");

    public override IEnumerable<GoapAction> GetProvidedActions() => actions;
}
