using VirtualVillage.Agents;
using VirtualVillage.Planning;

namespace VirtualVillage.Actions;

public class ScavengeAction : GoapAction
{
    private readonly string value;

    public ScavengeAction(string value, string tag, float cost) : base($"Scavenge[{value}]", cost)
    {
        Tags.Add(tag);
        this.value = value;
    }

    public override bool Precondition(WorldState state) => true;

    public override void Effect(WorldState state)
    {
        state.Inc(Agent.GetGenericStateKey(value), 1);
    }
}
