using VirtualVillage.Agents;
using VirtualVillage.Domain;
using VirtualVillage.Planning;

namespace VirtualVillage.Actions;

public class ScavengeAction : GoapAction
{
    private readonly string value;

    public ScavengeAction(string value, string tag, float cost, int duration) : base($"Scavenge[{value}]", cost, duration)
    {
        Tags.Add(tag);
        this.value = value;
    }

    public override bool Precondition(WorldState state) => true;

    public override void Effect(WorldState state)
    {
        state.Inc(Agent.GetGenericStateKey(value), 1);
    }

    public override void OnCompleted(World world, Agent agent)
    {
        if (agent.Inventory.TryGetValue(value, out var resource))
            agent.Inventory[value] = resource + 1;
        else
            agent.Inventory[value] = 1;
    }
}
