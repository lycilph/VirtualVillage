using VirtualVillage.Actions;

namespace VirtualVillage.Entities;

public abstract class WorldEntity
{
    public Position Position { get; }

    protected WorldEntity(Position position)
    {
        Position = position;
    }

    /// <summary>
    /// Called when an agent enters the world or replans.
    /// Entity can return zero or more actions for this agent.
    /// </summary>
    public abstract IEnumerable<GoapAction> GetActionsFor(Villager villager, World world);
}