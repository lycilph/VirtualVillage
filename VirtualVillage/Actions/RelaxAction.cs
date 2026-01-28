using VirtualVillage.Agents;
using VirtualVillage.Domain;
using VirtualVillage.Planning;

namespace VirtualVillage.Actions;

internal class RelaxAction : GoapAction
{
    public RelaxAction(float cost, int duration) : base("Relax", cost, duration)
    {
        Tags.Add(Keys.AllJobs);
    }

    public override void Effect(WorldState state) => state.Set(Agent.GetGenericStateKey(Keys.Relax), true);
}