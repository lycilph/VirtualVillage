using VirtualVillage.Domain;
using VirtualVillage.Entities;

namespace VirtualVillage.Actions;

public class MovementProvider(World world) : IActionProvider
{
    private readonly World world = world;

    public IEnumerable<GoapAction> GetProvidedActions()
    {
        return world.Entities
            .Select(e => new MoveToAction(e, 1))
            .ToList();
    }
}
