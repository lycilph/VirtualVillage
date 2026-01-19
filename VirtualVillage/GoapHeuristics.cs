namespace VirtualVillage;

public static class GoapHeuristics
{
    public static float DistanceToRelevantEntity(
        WorldState state,
        string agentId,
        GoapAction action)
    {
        if (action.TargetEntityId == null)
            return 0;

        var agent = state.Agents[agentId];
        var actionLocation = state.Entities[action.TargetEntityId].Location;

        return agent.Location.DistanceTo(actionLocation);
    }
}
