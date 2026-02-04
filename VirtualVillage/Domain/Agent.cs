using VirtualVillage.Actions;
using VirtualVillage.Core;
using VirtualVillage.Goals;
using VirtualVillage.Jobs;
using VirtualVillage.Planning;

namespace VirtualVillage.Domain;

public class Agent(string name, Job job, Location location) : WorldObject<Agent>(name, location)
{
    public Dictionary<string, int> Inventory { get; } = [];
    public Job Job { get; } = job;

    public Goal? CurrentGoal { get; private set; } = null;
    public Queue<GoapAction> CurrentPlan { get; private set; } = new();
    public GoapAction? CurrentAction { get; private set; } = null;
    public ActionContext? Context { get; private set; } = null;

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
        // -6 = padright(6)
        var info = $"{Name,-6} @ {Location,-7}";
        var goal = CurrentGoal != null ? $"Goal: {CurrentGoal.Name}" : "[No Goal]";
        var plan = $"Plan {CurrentPlan.Count} steps";
        var action = CurrentAction != null ? $"Action: {CurrentAction.Name}" : "[No Action]";
        var progress = Context != null && CurrentAction != null ? $"{Context.Elapsed} of {CurrentAction.Duration}" : "";
        var inventory = string.Join(", ", Inventory.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
        Console.WriteLine($"{info} {goal}, {plan}, {action}, {progress}, ({inventory})");
    }

    public void Replan(World world)
    {
        var state = world.GetWorldState(this);
        var actions = world.GetActions().Where(Job.AllowsAction).ToList();
        var goals = Job.GetGoals(world, this)
            .Where(g => g.IsValid(state))
            .OrderByDescending(g => g.Priority(state))
            .ToList();

        //var tracer = new MinimalConsolePlannerTracer();
        foreach (var goal in goals)
        {
            var plan = Planner.Plan(state, actions, goal);//, tracer);
            if (plan != null)
            {
                CurrentGoal = goal;
                CurrentPlan = new Queue<GoapAction>(plan);
                CurrentAction = null;
                Context = null;
                return;
            }
        }
    }

    public void Tick(World world)
    {
        if (CurrentAction != null && Context != null && CurrentAction.IsComplete(world, this, Context))
        {
            world.Events.Add($"{Name} completed {CurrentAction.Name}");
            CurrentAction = null;
            Context = null;
        }

        if (CurrentAction == null && CurrentPlan.Count == 0)
        {
            Replan(world);
            world.Events.Add($"{Name} replanned [New goal is {CurrentGoal?.Name}]");
        }

        if (CurrentAction == null)
        {
            CurrentAction = CurrentPlan.Dequeue();
            world.Events.Add($"{Name} started {CurrentAction.Name}");
        }


        if (!CurrentAction.CanExecute(world, this))
        {
            world.Events.Add($"{Name} {CurrentAction.Name} is invalid");
            CurrentPlan.Clear();
            CurrentAction = null;
            Context = null;
            return;
        }

        Context ??= CurrentAction.GetContext();
        CurrentAction.Execute(world, this, Context);
    }
}
