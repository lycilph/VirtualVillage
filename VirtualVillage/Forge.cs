namespace VirtualVillage;

public class Forge : Entity
{
    private readonly List<GoapAction> actions = [];

    public Forge(Location location) : base("Forge", location)
    {
        //var chop = new GoapAction.Builder("Chop Wood", 5)
        //    .WithPrecondition(s =>
        //        s.Get<Location>("agent_location").DistanceTo(Location) == 0 &&
        //        s.Get<int>("agent_axe") > 0 &&
        //        s.Get<int>(wood_key) > 0)
        //    .WithEffect(s =>
        //    {
        //        s.Inc("agent_wood", 1);
        //        s.Dec(wood_key, 1);
        //    })
        //    .WithEntity(this)
        //    .Build();
        //actions.Add(chop);
    }

    public override void Update(WorldState state) { }

    public override IEnumerable<GoapAction> GetProvidedActions() => actions;
}
