
namespace VirtualVillage;

public class MovementProvider(World world) : IActionProvider
{
    private readonly World world = world;

    public IEnumerable<GoapAction> GetProvidedActions()
    {
        var actions = new List<GoapAction>();

        foreach (var entity in world.Entities)
            actions.Add(
                new GoapAction.Builder($"MoveTo{entity}", 1)
                .WithPrecondition(s => s.Get<Location>("agent_location").DistanceTo(entity.Location) > 0)
                .WithEffect(s => s["agent_location"] = entity.Location )
                .WithTag("All")
                .Build());

        return actions;
    }
}
