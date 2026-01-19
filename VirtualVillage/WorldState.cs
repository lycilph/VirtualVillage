namespace VirtualVillage;

public class WorldState
{
    public Dictionary<string, AgentState> Agents { get; init; } = new();
    public Dictionary<string, EntityState> Entities { get; init; } = new();

    public WorldState Clone()
    {
        return new WorldState
        {
            Agents = Agents.ToDictionary(
                kv => kv.Key,
                kv => kv.Value with
                {
                    Inventory = new Dictionary<string, int>(kv.Value.Inventory)
                }),

            Entities = Entities.ToDictionary(
                kv => kv.Key,
                kv => kv.Value with
                {
                    Resources = new Dictionary<string, int>(kv.Value.Resources)
                })
        };
    }

    public override string ToString()
    {
        var agents = string.Join("|",
            Agents.Values.Select(a =>
                $"{a.Id}@{a.Location}[{string.Join(",", a.Inventory)}]"));

        var entities = string.Join("|",
            Entities.Values.Select(e =>
                $"{e.Id}:{e.Kind}@{e.Location}[{string.Join(",", e.Resources)}]"));

        return $"A:{agents} E:{entities}";
    }
}
