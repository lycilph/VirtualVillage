namespace VirtualVillage;

public static class GoapHeuristics
{
    public static float DistanceToRelevantEntity(
        WorldState state,
        string agentId,
        Predicate<WorldState> goal)
    {
        // If already at goal, cost is zero
        if (goal(state))
            return 0;

        var agent = state.Agents[agentId];

        // Estimate distance to nearest entity (safe lower bound)
        return state.Entities.Values
            .Select(e => agent.Location.DistanceTo(e.Location))
            .DefaultIfEmpty(0)
            .Min();
    }
}
