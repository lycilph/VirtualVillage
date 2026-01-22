using System.Text;

namespace VirtualVillage;

public class World
{
    private MovementProvider movementProvider;

    public List<Entity> Entities { get; } = [];
    public List<Agent> Agents { get; } = [];

    public World()
    {
        movementProvider = new MovementProvider(this);
    }

    public WorldState GetWorldState(Agent agent)
    {
        var state = new WorldState();
        
        agent.Update(state);

        foreach (var entity in Entities)
            entity.Update(state);

        return state;
    }

    public List<GoapAction> GetActions() => [.. Entities.SelectMany(e => e.GetProvidedActions()), .. movementProvider.GetProvidedActions()];

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine("Agents");
        foreach (var agent in Agents)
            sb.AppendLine(" * " + agent.ToString());

        sb.AppendLine("Entities");
        foreach (var entity in Entities)
        {
            sb.AppendLine(" * " + entity.ToString());
            var actions = entity.GetProvidedActions();
            foreach (var action in actions)
            {
                sb.AppendLine(" * * " + action);
            }
        }

        return sb.ToString();
    }
}
