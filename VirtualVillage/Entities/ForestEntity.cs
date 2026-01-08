using VirtualVillage.Actions;

namespace VirtualVillage.Entities;

public class ForestEntity : WorldEntity
{
    public ForestEntity(Position position) : base(position) { }

    public override IEnumerable<GoapAction> GetActionsFor(Villager villager, World world)
    {
        yield return new MoveAction(Position) { Source = this };
        yield return new ChopWoodAction(Position) { Source = this };
        yield return new GatherFoodAction(Position) { Source = this };
    }
}
