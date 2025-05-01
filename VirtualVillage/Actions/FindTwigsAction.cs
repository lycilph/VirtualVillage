using VirtualVillage.Objects;

namespace VirtualVillage.Actions;

public class FindTwigsAction : ActionBase
{
    public FindTwigsAction()
    {
        Name = "Find Twigs";
        Cost = 8;
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
