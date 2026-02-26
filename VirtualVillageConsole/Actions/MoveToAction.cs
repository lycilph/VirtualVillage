using VirtualVillageConsole.Core;

namespace VirtualVillageConsole.Actions;

public class MoveToAction(IEntity entity, float cost) : GoapAction($"MoveTo{entity.Name}[{entity.Id}]", cost, 0, entity)
{

    //public override bool Precondition(WorldState state)
    //{
    //    if (Entity == null) return false;

    //    return state.Get<Location>(Agent.GetGenericStateKey(Keys.Location)).DistanceTo(Entity.Location) > 0;
    //}

    //public override void Effect(WorldState state)
    //{
    //    if (Entity == null) return;

    //    state.Set(Agent.GetGenericStateKey(Keys.Location), Entity.Location);
    //}

    //public override void Execute(World world, Agent agent, ActionContext context)
    //{
    //    if (Entity == null) return;

    //    agent.Location = agent.Location.StepTowards(Entity.Location);
    //}

    //public override bool IsComplete(World world, Agent agent, ActionContext context) => Entity?.Location.DistanceTo(agent.Location) == 0;
}
