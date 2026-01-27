using VirtualVillage.Actions;
using VirtualVillage.Goals;

namespace VirtualVillage.Planning;

public class MinimalConsolePlannerTracer : IPlannerTracer
{
    public void ConsiderAction(GoapAction action) {}

    public void Enqueue(WorldState state, float g, float f) {}

    public void Expand(WorldState state, float g, float f) {}

    public void Finished(List<GoapAction>? plan)
    {
        Console.WriteLine();
        Console.WriteLine("=== PLAN RESULT ===");

        if (plan == null)
        {
            Console.WriteLine("No plan found.");
            return;
        }

        for (int i = 0; i < plan.Count; i++)
            Console.WriteLine($"{i + 1}. {plan[i].Name}");
    }

    public void GoalReached(WorldState state, int nodes_expanded) 
    {
        Console.WriteLine();
        Console.WriteLine($"=== GOAL REACHED (nodes expanded {nodes_expanded}) ===");
        Console.WriteLine(state);
    }

    public void Skip(string reason) {}

    public void Start(WorldState start, Goal goal)
    {
        Console.WriteLine("=== PLANNER START ===");
        Console.WriteLine($"Start: {start}");
        Console.WriteLine($"Goal : {goal.Name}");
        Console.WriteLine();
    }

    public void PlanReconstructed(List<GoapAction> plan, float cost) {}
}
