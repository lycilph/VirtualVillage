namespace VirtualVillage;

class Program
{
    static void Main()
    {
        var world = new WorldState() { Location = new() { Name = "Village", X = 0, Y = 0 } };

        var actionLibrary = new List<GoapAction> {
            new GoapAction("Buy Axe", 5, s => s.Location.Name == "Village" && s.Wood >= 5, s => { s.Wood -= 5; s.HasAxe = true; }),
            new GoapAction("Collect Twigs", 3, s => s.Location.Name == "Forest", s => s.Wood += 1),
            new GoapAction("Chop Timber", 1, s => s.Location.Name == "Forest" && s.HasAxe, s => s.Wood += 5),
            new GoapAction("Go to Forest", 2, s => s.Location.Name == "Village", s => s.Location.Name = "Forest"),
            new GoapAction("Go to Village", 2, s => s.Location.Name == "Forest", s => s.Location.Name = "Village")
        };

        // Goal: We want to be in the Village with at least 10 wood
        Predicate<WorldState> myGoal = s => s.Location.Name == "Village" && s.Wood >= 10;

        Console.WriteLine("Initial State: " + world);
        Console.WriteLine("Planning...");

        var plan = Planner.Plan(world, myGoal, actionLibrary);

        if (plan != null)
        {
            Console.WriteLine("\nPlan Found!");
            foreach (var action in plan)
            {
                Console.WriteLine($"Step: {action.Name}");
                action.Effect(world); // Apply to the real world
                Console.WriteLine("  Result: " + world);
            }
        }
        else
        {
            Console.WriteLine("No viable plan found.");
        }

        int iteration = 0;
        while (true)
        {
            //Console.Clear();
            //Console.WriteLine($"=== Village Status (iteration {iteration})===");
            //iteration++;

            Console.WriteLine();
            Console.Write("Step simulation [any key] or Quit [q]");
            var keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Q)
                break;
        }
    }
}
