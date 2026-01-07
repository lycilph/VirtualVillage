namespace VirtualVillage;

public static class PlanPrinter
{
    public static void PrintPlan(string actor, IEnumerable<GoapAction> plan)
    {
        Console.WriteLine($"Plan for {actor}:");
        foreach (var action in plan)
            Console.WriteLine($"  -> {action.Name}");
    }
}
