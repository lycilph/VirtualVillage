using Core.Goap;
using VirtualVillage.Objects;

namespace VirtualVillage.Actions;

public class FindTwigsAction : ActionBase
{
    private Twigs? twigs;

    public FindTwigsAction()
    {
        Name = "Find Twigs";
        Cost = 8;
        Effects = new Dictionary<string, object> { { "HasWood", true } };
    }

    public override void Update(World world, Villager villager)
    {
        twigs = world.GetClosest<Twigs>(villager.Position);
        Position = twigs.Position;
    }

    public override ActionResult Perform(World world, Villager villager)
    {
        if (twigs == null)
            throw new Exception("Tree must not be null");

        world.WorldObjects.Remove(twigs);

        villager.Inventory.AddResource(World.Wood);
        return ActionResult.Completed;
    }
}
