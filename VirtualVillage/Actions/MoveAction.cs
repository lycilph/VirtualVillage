using System.Diagnostics;
using System.Threading;

namespace VirtualVillage.Actions;

[DebuggerDisplay("{Name,nq}")]
public class MoveAction : GoapAction
{
    private Location targetLocation;

    public MoveAction(Location target) : base($"Move to {target}")
    {
        targetLocation = target;

        Preconditions.Add($"at{target}", false);

        // The effect is that we are now at the target location
        foreach (Location loc in Enum.GetValues<Location>())
            Effects.Add($"at{loc}", target == loc);
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
