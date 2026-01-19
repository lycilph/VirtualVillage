namespace VirtualVillage;

public class Simulation
{
    public WorldState World { get; }
    public List<IActionProvider> Providers { get; } = new();
    public List<SimAgent> Agents { get; } = new();

    public Simulation(WorldState world)
    {
        World = world;
    }

    public void Tick()
    {
        // Tick each agent
        foreach (var agent in Agents)
        {
            var actions = Providers
                .SelectMany(p => p.GetActions(agent.Id, World))
                .ToList();

            agent.Tick(World, actions);
        }
    }
}