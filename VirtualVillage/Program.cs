using VirtualVillage.Entities;

namespace VirtualVillage;

internal class Program
{
    static void Main()
    {
        var providers = new List<IActionProvider> { new Forest(), new ToolShed() };
        var actions = providers.SelectMany(p => p.GetActions()).ToList();
        var villager = new Agent("Lumberjack", Position.Zero);

        while (villager.State.Get("Logs") < 3)
        {
            villager.Update(actions);
            Thread.Sleep(100);
        }
        Console.WriteLine("Goal Reached! The village has wood.");
        Console.ReadKey();
    }
}
