using VirtualVillage.Actions;

namespace VirtualVillage.Entities;

public class MineEntity : WorldEntity
{
    public MineEntity(Position position) : base(position) { }

    public override IEnumerable<GoapAction> GetActionsFor(Villager villager, World world)
    {
        yield return new MoveAction(Position) { Source = this };
        yield return new MineOreAction(Position) { Source = this };
    }
}
