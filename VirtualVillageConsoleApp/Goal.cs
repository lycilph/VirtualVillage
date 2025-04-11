namespace VirtualVillageConsoleApp;

public class Goal
{
    public WorldState State { get; set; } = new();

    public virtual bool IsValid() => true;
}
