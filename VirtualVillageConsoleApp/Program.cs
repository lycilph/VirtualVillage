namespace VirtualVillageConsoleApp;

internal class Program
{
    static Simulation sim = new();

    static void Main(string[] args)
    {
        SetupSimulation();

        var sim_running = true;
        while (sim_running)
        {
            Console.Clear();
            Console.WriteLine("1. Advance simulation");
            Console.WriteLine("2. Quit");

            sim.Render();

            var key = Console.ReadKey();
            switch (key.KeyChar)
            {
                case '1':
                    sim.Update();
                    break;
                case '2':
                    sim_running = false;
                    break;
            }
        }
    }

    private static void SetupSimulation()
    {
        var agent = new Agent { Name = "Bob" };
        agent.State.Set("hunger", 0);
        agent.State.Set("food", 0);
        sim.Agents.Add(agent);

        var tree = new Item { Name = "Tree", Position = new Position(10, 10) };
        sim.Items.Add(tree);

        var home = new Item { Name = "Home", Position = new Position(0, 0) };
        sim.Items.Add(home);
    }
}
