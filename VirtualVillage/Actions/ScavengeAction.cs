using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Entities;
using VirtualVillage.Planning;

namespace VirtualVillage.Actions;

public class ScavengeAction : GoapAction
{
    private readonly string value;

    public ScavengeAction(string value, string tag, float cost, int duration, IEntity entity) : base($"Scavenge[{value}]", cost, duration, entity)
    {
        Tags.Add(tag);
        this.value = value;
    }

    public override bool Precondition(WorldState state)
    {
        if (Entity == null) return false;

        return state.Get<Location>(Agent.GetGenericStateKey(Keys.Location)).DistanceTo(Entity.Location) == 0;
    }

    public override void Effect(WorldState state)
    {
        state.Inc(Agent.GetGenericStateKey(value), 1);
    }

    public override void OnCompleted(World world, Agent agent)
    {
        if (value.Equals(Keys.Wood) && Entity is ScavengeWoodLocation twigs)
            twigs.Amount -= 1;

        if (value.Equals(Keys.Ore) && Entity is ScavengeOreLocation nuggets)
            nuggets.Amount -= 1;

        if (agent.Inventory.TryGetValue(value, out var resource))
            agent.Inventory[value] = resource + 1;
        else
            agent.Inventory[value] = 1;
    }
}
