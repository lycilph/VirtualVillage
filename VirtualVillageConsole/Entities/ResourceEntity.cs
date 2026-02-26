using VirtualVillageConsole.Actions;
using VirtualVillageConsole.Core;

namespace VirtualVillageConsole.Entities;

public class ResourceEntity : EntityBase<ResourceEntity>
{
    public int Amount { get; set; }

    private readonly GoapAction moveAction;
    private GoapAction collectAction = new NoOpAction();

    public ResourceEntity(string name, Location location, int amount) : base(name, location)
    {
        MustBeReserved = true;
        Amount = amount;
        
        moveAction = new MoveToAction(this, 1);
    }

    public void SetCollectionAction(GoapAction action) => collectAction = action;
    
    public override IEnumerable<GoapAction> GetProvidedActions() => ReservedBy == -1 ? [moveAction, collectAction] : [];
}
