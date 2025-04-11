namespace VirtualVillageConsoleApp;

public class Agent
{
    public string Name { get; set; } = string.Empty;
    public Position Position { get; set; } = new Position();
    public WorldState State { get; set; } = new();
    public IEnumerable<Goal> Goals { get; set; } = [];
    public IEnumerable<Action> AvailableActions { get; set; } = [];

    public void Update()
    {
        State.Set("hunger", State["hunger"] + 1);
    }

    public void Render()
    {
        Console.WriteLine($"Agent {Name} {Position}");
        foreach (var pair in State.states)
            Console.WriteLine($" * {pair.Key} {pair.Value}");
    }
}
