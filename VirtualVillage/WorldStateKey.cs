using VirtualVillage;

public static class WorldStateKey
{
    //public static string Compute(WorldState state)
    //    => state.ToString();
    public static int ComputeHash(WorldState state)
    {
        var hash = new HashCode();

        // agents...
        foreach (var (id, agent) in state.Agents.OrderBy(kv => kv.Key))
        {
            hash.Add(id);
            hash.Add(agent.Location);
            foreach (var (item, count) in agent.Inventory.OrderBy(kv => kv.Key))
            {
                hash.Add(item);
                hash.Add(count);
            }
        }

        // entities...
        foreach (var (id, entity) in state.Entities.OrderBy(kv => kv.Key))
        {
            hash.Add(id);
            hash.Add(entity.Kind);
            foreach (var (item, count) in entity.Resources.OrderBy(kv => kv.Key))
            {
                hash.Add(item);
                hash.Add(count);
            }
        }

        return hash.ToHashCode();
    }
}