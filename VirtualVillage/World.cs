using System.Text;
using VirtualVillage.Entities;

namespace VirtualVillage;

public class World
{
    private readonly MovementProvider movementProvider;
    private readonly DefaultActionsProvider defaultActionsProvider;

    public List<Entity> Entities { get; } = [];
    public List<Agent> Agents { get; } = [];

    public World()
    {
        movementProvider = new MovementProvider(this);
        defaultActionsProvider = new DefaultActionsProvider();
    }

    public WorldState GetWorldState(Agent agent)
    {
        var state = new WorldState();
        
        agent.Update(state);

        foreach (var entity in Entities)
            entity.Update(state);

        return state;
    }

    public List<GoapAction> GetActions() => 
        [.. Entities.SelectMany(e => e.GetProvidedActions()), 
         .. movementProvider.GetProvidedActions(), 
         .. defaultActionsProvider.GetProvidedActions()];

    public void Render()
    {
        Console.WriteLine($"World:");
        foreach (var entity in Entities)
            entity.Render();
    }

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

        sb.AppendLine("Movement actions:");
        foreach (var action in movementProvider.GetProvidedActions())
            sb.AppendLine(" * " + action);

        sb.AppendLine("Default actions:");
        foreach (var action in defaultActionsProvider.GetProvidedActions())
            sb.AppendLine(" * " + action);

        return sb.ToString();
    }
}
