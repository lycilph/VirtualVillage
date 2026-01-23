using VirtualVillage.Agents;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Entities;
using VirtualVillage.Planning;

namespace VirtualVillage.Actions;

public class MoveToAction : GoapAction
{
    public MoveToAction(IEntity entity, float cost) : base($"MoveTo{entity}", cost, entity)
    {
        Tags.Add(Keys.AllJobs);
    }

    public override bool Precondition(WorldState state)
    {
        if (Entity == null) return false;

        return state.Get<Location>(Agent.GetGenericStateKey(Keys.Location)).DistanceTo(Entity.Location) > 0;
    }

    public override void Effect(WorldState state)
    {
        if (Entity == null) return;

        state.Set(Agent.GetGenericStateKey(Keys.Location), Entity.Location);
    }
}
