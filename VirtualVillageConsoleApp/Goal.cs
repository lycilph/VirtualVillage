namespace VirtualVillageConsoleApp;

public class Goal
{
    public string Name { get; set; } = string.Empty;
    public int Priority { get; set; } = 0; // Higher number = higher priority
    public WorldState GoalState { get; set; } = new();
}
