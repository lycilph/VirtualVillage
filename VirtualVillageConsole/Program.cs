using VirtualVillageConsole.Entities;

namespace VirtualVillageConsole;

internal class Program
{
    static void Main()
    {
        /*
        
        Algorithm:
        1. Tick World
            a. Tick Entities
            b. Tick Agents
        2. Tick Orchestrator
        3. Update goals on blackboard
         
         */


        int iteration = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"=== Simulation Tick {iteration} ===");
            Console.WriteLine();
            iteration++;

            Console.WriteLine();
            Console.WriteLine("=== Options ===");
            Console.WriteLine("[any] Step");
            Console.WriteLine("[p] show agent plan");
            Console.WriteLine("[q] Quit");
            Console.Write("Enter choice: ");
            var keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.Q:
                    return;
                case ConsoleKey.P:
                    ShowAgentPlan();
                    break;
            }
        }
    }

    private static void ShowAgentPlan()
    {
        Console.WriteLine();

        for (int i = 0; i < 3; i++)
            Console.WriteLine($"[{i}] Agent {i}");

        Console.Write("Enter choice: ");
        var n = Console.ReadLine();
    }
}
