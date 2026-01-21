
namespace VirtualVillage;

public class Forest(Location location) : Entity("Forest", location)
{
    public override IEnumerable<GoapAction> GetProvidedActions()
    {
        yield return new GoapAction(
            "Chop Wood",
            5,
            s => s.TryGetValue("agent_location", out var value) && 
                value is Location agent_location && 
                Location.DistanceTo(agent_location) == 0,
            s => { },
            this);
    }
}
