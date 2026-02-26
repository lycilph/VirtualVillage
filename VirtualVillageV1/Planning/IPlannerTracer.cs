using VirtualVillage.Actions;
using VirtualVillage.Goals;

namespace VirtualVillage.Planning;

public interface IPlannerTracer
{
    void Start(WorldState start, Goal goal);
    void Expand(WorldState state, float g, float f);
    void ConsiderAction(GoapAction action);
    void Skip(string reason);
    void Enqueue(WorldState state, float g, float f);
    void GoalReached(WorldState state, int nodes_expanded);
    void Finished(List<GoapAction>? plan);
    void PlanReconstructed(List<GoapAction> plan, float cost);
}