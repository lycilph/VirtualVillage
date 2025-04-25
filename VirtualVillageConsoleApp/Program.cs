using VirtualVillageConsoleApp.Goap;

namespace VirtualVillageConsoleApp;

internal class Program
{
    static void Main()
    {
        List<GoapAction> actions =
            [
                new GoapAction
                {
                    Name = "Chop Wood",
                    Cost = 4,
                    Preconditions = { { "HasTool", true } },
                    Effects = { { "HasWood", true } },
                },
                new GoapAction
                {
                    Name = "Find Twigs",
                    Cost = 8,
                    Effects = { { "HasWood", true } },
                },
                new GoapAction
                {
                    Name = "Store Resource [Wood]",
                    Cost = 1,
                    Preconditions = { { "HasWood", true } },
                    Effects = { { "StoredWood", true } },
                },
                new GoapAction
                {
                    Name = "Get Resource [Wood]",
                    Cost = 1,
                    Preconditions = { { "StoredWood", true } },
                    Effects = { { "HasWood", true } },
                },
                new GoapAction
                {
                    Name = "Get Resource [Tool]",
                    Cost = 1,
                    Preconditions = { { "StoredTool", true } },
                    Effects = { { "HasTool", true } },
                }
            ];
        GoapGoal goal = new()
        {
            Name = "Gather Wood",
            State = { { "StoredWood", true } }
        };
        Dictionary<string, bool> world_state = new() { { "StoredTool", true } };

        GoapPlanner planner = new();
        var plan = planner.GetPlan(goal, world_state, actions);

        foreach (var action in plan)
            Console.WriteLine($"Action {action.Name}");

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
