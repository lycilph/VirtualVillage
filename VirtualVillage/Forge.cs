namespace VirtualVillage;

public class Forge : Entity
{
    private readonly List<GoapAction> actions = [];

    public Forge(Location location) : base("Forge", location)
    {
        actions.Add(
            new GoapAction.Builder("Craft Axe", 5)
            .WithPrecondition(s =>
                s.Get<Location>("agent_location").DistanceTo(Location) == 0 &&
                s.Get<int>("agent_wood") > 0 &&
                s.Get<int>("agent_ore") > 0)
            .WithEffect(s =>
            {
                s.Dec("agent_wood", 1);
                s.Dec("agent_ore", 1);
                s.Inc("agent_axe", 1);
            })
            .WithEntity(this)
            .WithTag("Blacksmith")
            .Build());
    }

    public override void Update(WorldState state) { }

    public override IEnumerable<GoapAction> GetProvidedActions() => actions;
}
