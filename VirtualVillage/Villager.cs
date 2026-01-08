using VirtualVillage.Actions;

namespace VirtualVillage;

public class Villager
{
    public string Name { get; }
    public Position Position { get; private set; }
    public List<GoapAction> AvailableActions { get; set; } = [];

    private Queue<GoapAction> plan = new();
    private int remaining;

    public GoapState State { get; }

    public Villager(string name, Position start)
    {
        Name = name;
        Position = start;

        State = new GoapState();
        State.Set(WorldKeys.Position, start);
        State.Set(WorldKeys.HasFood, false);
    }

    public void CollectActions(World world)
    {
        AvailableActions.Clear();

        foreach (var entity in world.Entities)
        {
            foreach (var action in entity.GetActionsFor(this, world))
                AvailableActions.Add(action);
        }
    }

    public void SetPlan(IEnumerable<GoapAction> actions)
    {
        plan = new Queue<GoapAction>(actions);
    }

    public void Update(World world)
    {
        if (remaining > 0)
        {
            remaining--;
            return;
        }

        if (!plan.Any())
        {
            Console.WriteLine($"{world.Tick}: {Name} is idle");
            return;
        }

        var action = plan.Dequeue();
        // Capture state BEFORE Apply
        var context = new ActionExecutionContext(State.Clone());

        Console.WriteLine($"{world.Tick}: {Name} starts {action.Name}");

        if (action is MoveAction move)
        {
            remaining = Position.DistanceTo(move.TargetPosition);
            Position = move.TargetPosition;
        }
        else
        {
            remaining = action.GetCost(State);
        }

        // Update belief state
        action.Apply(State);
        State.Set(WorldKeys.Position, Position);

        // 🔑 Let the action affect the world
        action.Execute(world, this, context);
    }
}
