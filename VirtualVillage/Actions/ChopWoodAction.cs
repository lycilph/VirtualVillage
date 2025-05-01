using VirtualVillage.Objects;

namespace VirtualVillage.Actions;

public class ChopWoodAction : ActionBase
{
    public ChopWoodAction()
    {
        Name = "Chop Wood";
        Cost = 4;
        Preconditions = new Dictionary<string, object> { { "HasTool", true } };
        Effects = new Dictionary<string, object> { { "HasWood", true } };
    }

    public override void Update(World world, Villager villager)
    {
        var tree = world.GetClosest<Tree>(villager.Position);
        Position = tree.Position;
    }

    public override ActionResult Perform(World world, Villager villager)
    {
        villager.AddWood(1);
        return ActionResult.Completed;
    }
}
