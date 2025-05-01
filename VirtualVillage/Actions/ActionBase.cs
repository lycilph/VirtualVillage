using Core.Goap;

namespace VirtualVillage.Actions;

public class ActionBase : GoapAction
{
    public enum ActionResult { Completed, Failed };

    public virtual void Update(World world, Villager villager) { }
    public virtual ActionResult Perform(World world, Villager villager) => ActionResult.Completed;
}
