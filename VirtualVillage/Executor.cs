namespace VirtualVillage;

public static class Executor
{
    public static void ExecutePlan(
        WorldState world,
        string agentId,
        List<GoapAction> plan,
        bool debug = true)
    {
        foreach (var action in plan)
        {
            if (debug)
            {
                Console.WriteLine(
                    $"Agent {agentId} executing: {action}");
            }

            // Apply action effect to live world
            action.Effect(world);

            if (debug)
            {
                var agent = world.Agents[agentId];
                Console.WriteLine(
                    $"  Agent location: {agent.Location} | Inventory: {string.Join(", ", agent.Inventory)}");
            }
        }
    }
}
