using VirtualVillage.Agents;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Entities;
using VirtualVillage.Jobs;

namespace VirtualVillage;

class Program
{
    static void Main()
    {
        var lumberjack = new Agent("Bjorn Oakhand", new LumberjackJob(), new Location(0, 0));
        var miner = new Agent("Borin Ironvein", new MinerJob(), new Location(0, 0));
        var blacksmith = new Agent("Magnus Ironwright", new BlacksmithJob(), new Location(0, 0));
        var forest = new Forest(new Location(5, 5), 5);
        var mine = new Mine(new Location(-5, 2), 10);
        var storehouse = new Storehouse(new Location(0, 1));
        var forge = new Forge(new Location(-2, -1));

        var world = new World();
        world.Agents.Add(lumberjack);
        world.Agents.Add(miner);
        world.Agents.Add(blacksmith);
        world.Entities.Add(forest);
        world.Entities.Add(mine);
        world.Entities.Add(storehouse);
        world.Entities.Add(forge);
        Console.WriteLine(world);

        int iteration = 0;
        while (true)
        {
            Console.WriteLine($"\n=== Simulation Tick {iteration} ===");
            iteration++;

            foreach (var agent in world.Agents)
                agent.Tick(world);
            world.Render();

            Console.WriteLine();
            Console.Write("Step simulation [any key] or Quit [q]");
            var keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Q)
                break;
        }
    }
}
