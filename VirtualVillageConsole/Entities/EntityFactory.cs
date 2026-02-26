using VirtualVillageConsole.Actions;
using VirtualVillageConsole.Core;

namespace VirtualVillageConsole.Entities;

public static class EntityFactory
{
    public static IEntity CreateForestEntity()
    {
        var entity = new ResourceEntity("Forest", Location.Random(), 5);
        entity.SetCollectionAction(new CollectFirewoodAction("Collect Firewood", 1, 1, entity));

        return entity;
    }
}
