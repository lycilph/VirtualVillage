using VirtualVillage.Domain;
using VirtualVillage.Entities;

namespace VirtualVillage.Actions;

public class MovementProvider(World world) : IActionProvider
{
    private readonly World world = world;

    private readonly Dictionary<int, GoapAction> cachedActions = [];

    public IEnumerable<GoapAction> GetProvidedActions()
    {
        var entity_ids = world.Entities.Select(e => e.Id).ToList();

        // Find movement actions missing
        var entities_missing_actions = entity_ids.Except(cachedActions.Keys).ToList();
        foreach (var id in entities_missing_actions)
        {
            var entity = world.Entities.First(e => e.Id == id);
            cachedActions.Add(id, new MoveToAction(entity, 1));
        }

        // Get actions for each entity
        var result = cachedActions.Values.ToList();
        return result;
    }
}
