namespace VirtualVillage;

public class SimAgent
{
    public string Id { get; }
    public Predicate<WorldState> Goal { get; set; }

    public List<GoapAction> CurrentPlan { get; private set; } = [];
    private int planIndex = 0;

    public SimAgent(string id, Predicate<WorldState> goal)
    {
        Id = id;
        Goal = goal;
    }

    public void Tick(WorldState world, List<GoapAction> availableActions)
    {
        // Re-plan if needed
        if (CurrentPlan.Count == 0 || planIndex >= CurrentPlan.Count)
        {
            CurrentPlan = Planner.Plan(world, Id, Goal, availableActions, debug: false);
            planIndex = 0;
            if (CurrentPlan.Count == 0)
            {
                Console.WriteLine($"Agent {Id}: No plan found for current goal.");
                return;
            }
        }

        // Execute the next action
        var action = CurrentPlan[planIndex];

        // Check if action is still valid
        if (action.Precondition(world))
        {
            Console.WriteLine($"Agent {Id} executes: {action}");
            action.Effect(world);
            planIndex++;
        }
        else
        {
            Console.WriteLine($"Agent {Id} {action} is invalid");
            CurrentPlan.Clear();
        }
    }
}
