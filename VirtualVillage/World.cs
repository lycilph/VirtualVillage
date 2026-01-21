using System.Text;

namespace VirtualVillage;

public class World
{
    public List<Entity> Entities { get; } = [];
    public List<Agent> Agents { get; } = [];

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
