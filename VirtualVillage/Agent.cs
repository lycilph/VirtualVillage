using VirtualVillage.Jobs;

namespace VirtualVillage;

public class Agent(string name, Job job, Location location)
{
    public int Id { get; } = IdGenerator.Next();
    public string Name { get; } = name;
    public Location Location { get; } = location;
    public Job Job { get; } = job;

    public Queue<GoapAction> CurrentPlan { get; private set; } = new();
    public ExecutionState State { get; private set; } = ExecutionState.Idle;

    public void Update(WorldState state)
    {
        state["agent_location"] = Location;
    }

    public void AssignPlan(IEnumerable<GoapAction> plan)
    {
        CurrentPlan = new Queue<GoapAction>(plan);
        State = ExecutionState.Executing;
    }

    public void Tick(World world)
    {
        if (State != ExecutionState.Executing)
            return;

        if (CurrentPlan.Count == 0)
        {
            State = ExecutionState.Idle;
            return;
        }

        var action = CurrentPlan.Peek();

        if (!action.CanExecute(world, this))
        {
            State = ExecutionState.Failed;
            CurrentPlan.Clear();
            return;
        }

        action.Execute(world, this);
        Console.WriteLine($"{Name} executes {action.Name} at {Location}");

        if (action.IsComplete(world, this))
        {
            CurrentPlan.Dequeue();
        }
    }

    public override string ToString() => $"{Name}[{Id}] {Location}";
}
