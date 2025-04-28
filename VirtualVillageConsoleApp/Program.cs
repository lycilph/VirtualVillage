using VirtualVillageConsoleApp.Goap;
using VirtualVillageConsoleApp.Simulation;

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
                    Position = new(5, 5), // Should be update to the nearest tree
                    Preconditions = { { "HasTool", true } },
                    Effects = { { "HasWood", true } },
                },
                new GoapAction
                {
                    Name = "Find Twigs",
                    Cost = 8,
                    Position = new(37, 12), // This should be updated to a random position each time the planner is run
                    Effects = { { "HasWood", true } },
                },
                new GoapAction
                {
                    Name = "Store Resource [Wood]",
                    Cost = 1,
                    Position = new(50, 40), // Storehouse location
                    Preconditions = { { "HasWood", true } },
                    Effects = { { "StoredWood", true } },
                },
                new GoapAction
                {
                    Name = "Get Resource [Tool]",
                    Cost = 1,
                    Position = new(50, 40), // Storehouse location
                    Preconditions = { { "StoredTool", true } },
                    Effects = { { "HasTool", true } },
                }
            ];
        GoapGoal goal = new()
        {
            Name = "Gather Wood",
            State = { { "StoredWood", true } }
        };
        Dictionary<string, object> world_state = new() { { "StoredTool", true } };
        Agent agent = new Agent(new Position(40, 40));

        GoapPlanner planner = new();
        var plan = planner.GetPlan(agent, goal, world_state, actions, true);

        foreach (var action in plan.Actions)
            Console.WriteLine($"Action {action.Name}");

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
