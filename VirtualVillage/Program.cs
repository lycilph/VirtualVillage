using VirtualVillage.Actions;

namespace VirtualVillage;

class Program
{
    //try gemini version

    static void Main()
    {
        Console.WriteLine("Simulation started.");

        var v1 = new Villager { Name = "Eldric" };
        var v2 = new Villager { Name = "Gunther" };

        // Both want to deliver wood to the village
        v1.currentGoal.Add("deliveredWood", true);
        v2.currentGoal.Add("deliveredWood", true);

        // Give them both the same set of abilities
        var actions = new List<GoapAction> {
            new PickUpAxeAction(),
            new ChopWoodAction(),
            new DropOffWoodAction(),
            new EatAction(),
        };
        // Add the move actions
        foreach (Location loc in Enum.GetValues<Location>())
        {
            actions.Add(new MoveAction(loc));
        }

        v1.AvailableActions = actions;
        v2.AvailableActions = actions;

        while (true)
        {
            Console.WriteLine();
            Storehouse.PrintStatus();

            v1.Update();
            v2.Update();

            Console.WriteLine();
            Console.WriteLine("Step simulation [Enter] or Quit [q]");
            var keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Q)
                break;
        }
    }
}
