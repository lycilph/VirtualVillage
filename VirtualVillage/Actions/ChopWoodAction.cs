using Core.Goap;
using VirtualVillage.Objects;

namespace VirtualVillage.Actions;

public class ChopWoodAction : ActionBase
{
    private Tree? tree;

    public ChopWoodAction()
    {
        Name = "Chop Wood";
        Cost = 4;
        Duration = 5;
        Preconditions = new Dictionary<string, object> { { $"Has{World.Tools}", true } };
        Effects = new Dictionary<string, object> { { $"Has{World.Wood}", true } };
    }

    public override void Update(World world, Villager villager)
    {
        tree = world.GetClosest<Tree>(villager.Position);
        Position = tree.Position;
    }

    public override ActionResult Perform(World world, Villager villager)
    {
        if (tree == null)
            throw new Exception("Tree must not be null");

        tree.Health--;
        if (tree.Health <= 0)
            world.WorldObjects.Remove(tree);

        villager.Inventory.AddResource(World.Wood);
        return ActionResult.Completed;
    }
}
