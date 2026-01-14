namespace VirtualVillage.Actions;

public class MoveAction : GoapAction
{
    private Location targetLocation;

    public MoveAction(Location target) : base($"Move to {target}")
    {
        targetLocation = target;
        // The effect is that we are now at the target location
        Effects.Add($"at{target}", true);
        Cost = 1f;
    }

    public override bool IsPossible(Villager agent)
    {
        // Only move if we aren't already there
        return agent.CurrentLocation != targetLocation;
    }

    public override void Execute(Villager agent)
    {
        agent.CurrentLocation = targetLocation;
        Console.WriteLine($"{agent.Name} walked to the {targetLocation}.");
    }
}
