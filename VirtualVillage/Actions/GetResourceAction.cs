using System.Numerics;
using Core.Goap;
using VirtualVillage.Objects;

namespace VirtualVillage.Actions;

public class GetResourceAction : ActionBase
{
    private string resource;

    public GetResourceAction(string resource, Vector2 position)
    {
        this.resource = resource;

        Name = $"Get Resource [{resource}]";
        Position = position;
        Cost = 1;
        Duration = 1;
        Preconditions = new Dictionary<string, object> { { $"Stored{resource}", true } };
        Effects = new Dictionary<string, object> { { $"Has{resource}", true } };
    }

    public override ActionResult Perform(World world, Villager villager)
    {
        var storehouse = world.Get<Storehouse>();
        if (!storehouse.Inventory.Has(resource))
            return ActionResult.Failed;

        villager.Inventory.AddResource(resource);
        storehouse.Inventory.RemoveResource(resource);

        return ActionResult.Completed;
    }
}
