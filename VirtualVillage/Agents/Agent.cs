using VirtualVillage.Actions;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Jobs;
using VirtualVillage.Planning;

namespace VirtualVillage.Agents;

public class Agent(string name, Job job, Location location) : WorldObject<Agent>(name, location)
{
    public Dictionary<string, int> Inventory { get; } = [];
    public Job Job { get; } = job;

    public Queue<GoapAction> CurrentPlan { get; private set; } = new();
    public ExecutionState State { get; private set; } = ExecutionState.Idle;

    public override void Update(WorldState state)
    {
        state[GetGenericStateKey(Keys.Location)] = Location;

        foreach (var kvp in Inventory)
        {
            if (kvp.Value > 0)
                state[GetGenericStateKey(kvp.Key)] = kvp.Value;
        }
    }

    public override void Render()
    {
        var resources = string.Join(", ", Inventory.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
        Console.WriteLine($"Agent {Name} {State} ({resources})");
    }

    public void Replan(World world)
    {
        var tracer = new MinimalConsolePlannerTracer();

        var state = world.GetWorldState(this);
        var actions = world.GetActions().Where(Job.AllowsAction).ToList();
        var goal = Job.GetGoals(world, this).First();
        var plan = Planner.Plan(state, actions, goal);//, tracer);

        if (plan != null)
        {
            CurrentPlan = new Queue<GoapAction>(plan);
            State = ExecutionState.Executing;
        }
    }

    public void Tick(World world)
    {
        if (State != ExecutionState.Executing)
            Replan(world);

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
