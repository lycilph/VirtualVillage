namespace VirtualVillageConsoleApp;

public class Agent
{
    public string Name { get; set; } = string.Empty;
    public Position Position { get; set; } = new Position();

    public WorldState State { get; set; } = new();
    
    public List<Goal> Goals { get; set; } = [];
    public List<Action> AvailableActions { get; set; } = [];

    public Goal? CurrentGoal { get; set; } = null;
    public Action? CurrentAction { get; set; } = null;
    // Plan? (ie. a list of all the actions to reach the current goal)

    public void Update()
    {
        State.Set("hunger", State["hunger"] + 1);
    }

    public void Render()
    {
        Console.WriteLine($"Agent {Name} {Position}");
        foreach (var pair in State.states)
            Console.WriteLine($" * {pair.Key} {pair.Value}");
        foreach (var goal in Goals)
            Console.WriteLine($" * Goal {goal.GetType()}");
    }
}
