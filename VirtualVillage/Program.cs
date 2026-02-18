using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Entities;
using VirtualVillage.Jobs;

namespace VirtualVillage;

class Program
{
    static void Main()
    {
        var lumberjack1 = new Agent("Bjorn", new LumberjackJob(), new Location(0, 0));
        var lumberjack2 = new Agent("Beiner", new LumberjackJob(), new Location(0, 5));
        var miner = new Agent("Borin", new MinerJob(), new Location(0, 0));
        var blacksmith = new Agent("Magnus", new BlacksmithJob(), new Location(0, 0));
        var forest = new Forest(new Location(5, 5), 5);
        var mine = new Mine(new Location(-5, 2), 10);
        var storehouse = new Storehouse(new Location(0, 1));
        var forge = new Forge(new Location(-2, -1));
        var twigs = new ScavengeWoodLocation(Location.Random());
        var nuggets = new ScavengeOreLocation(Location.Random());

        var world = new World();
        world.Agents.Add(lumberjack1);
        world.Agents.Add(lumberjack2);
        world.Agents.Add(miner);
        world.Agents.Add(blacksmith);
        world.Entities.Add(forest);
        world.Entities.Add(mine);
        world.Entities.Add(storehouse);
        world.Entities.Add(forge);
        world.Entities.Add(twigs);
        world.Entities.Add(nuggets);

        int iteration = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"=== Simulation Tick {iteration} ===");
            Console.WriteLine();
            iteration++;

            world.Tick();
            world.Render();

            Console.WriteLine();
            Console.WriteLine("=== Options ===");
            Console.WriteLine("[any] Step");
            Console.WriteLine("[q] Quit");
            Console.Write("Enter choice: ");
            var keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.Q:
                    return;
            }
        }
    }
}
