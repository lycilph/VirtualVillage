using VirtualVillage.Actions;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Jobs;
using VirtualVillage.Planning;

namespace VirtualVillage.Agents;

public class Agent(string name, Job job, Location location) : WorldObject<Agent>(name, location)
{
    public Job Job { get; } = job;

    public Queue<GoapAction> CurrentPlan { get; private set; } = new();
    public ExecutionState State { get; private set; } = ExecutionState.Idle;

    public override void Update(WorldState state)
    {
        state[GetGenericStateKey(Keys.Location)] = Location;
    }

    public override void Render()
    {
        Console.WriteLine("Agent ...");
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
}
