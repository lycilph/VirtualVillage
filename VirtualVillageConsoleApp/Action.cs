namespace VirtualVillageConsoleApp;

public class Action
{
    public string Name { get; set; } = string.Empty;
    public object? Target { get; set; } = null;
    public WorldState Preconditions = new();
    public WorldState Effects = new();
}
