using System.Numerics;
using Core.Goap;
using VirtualVillage.Objects;

namespace VirtualVillage.Actions;

public class StoreResourceAction : ActionBase
{
    private string resource;

    public StoreResourceAction(string resource, Vector2 position)
    {
        this.resource = resource;

        Name = $"Store Resource [{resource}]";
        Position = position;
        Cost = 1;
        Preconditions = new Dictionary<string, object> { { $"Has{resource}", true } };
        Effects = new Dictionary<string, object> { { $"Stored{resource}", true } };
    }

    public override ActionResult Perform(World world, Villager villager)
    {
        if (!villager.Inventory.Has(resource))
            return ActionResult.Failed;

        villager.Inventory.Remove(resource);

        var storehouse = world.Get<Storehouse>();
        storehouse.Inventory.AddResource(resource);

        return ActionResult.Completed;
    }
}
