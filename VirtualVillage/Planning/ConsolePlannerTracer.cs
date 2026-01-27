using System.Xml.Linq;
using VirtualVillage.Actions;
using VirtualVillage.Goals;

namespace VirtualVillage.Planning;

public sealed class ConsolePlannerTracer : IPlannerTracer
{
    private int depth = 0;

    private string Indent => new(' ', depth * 2);

    public void Start(WorldState start, Goal goal)
    {
        Console.WriteLine("=== PLANNER START ===");
        Console.WriteLine($"Start: {start}");
        Console.WriteLine($"Goal : {goal.Name}");
        Console.WriteLine();
    }

    public void Expand(WorldState state, float g, float f)
    {
        Console.WriteLine($"{Indent}Expand  g={g} f={f}");
        Console.WriteLine($"{Indent}{state}");
        depth++;
    }

    public void ConsiderAction(GoapAction action)
    {
        Console.WriteLine($"{Indent}Try action: {action.Name}");
    }

    public void Skip(string reason)
    {
        Console.WriteLine($"{Indent}↳ Skip: {reason}");
    }

    public void Enqueue(WorldState state, float g, float f)
    {
        Console.WriteLine($"{Indent}↳ Enqueue g={g} f={f}");
        Console.WriteLine($"{Indent}{state}");
    }

    public void GoalReached(WorldState state, int nodes_expanded)
    {
        Console.WriteLine();
        Console.WriteLine($"=== GOAL REACHED (nodes expanded {nodes_expanded}) ===");
        Console.WriteLine(state);
    }

    public void Finished(List<GoapAction>? plan)
    {
        depth = 0;
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

    public void PlanReconstructed(List<GoapAction> plan, float cost)
    {
        Console.WriteLine($"Found plan (total cost {cost}, {plan.Count} steps)");
    }
}
