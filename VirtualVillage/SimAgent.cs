namespace VirtualVillage;

public class SimAgent
{
    public string Id { get; }
    public Predicate<WorldState> PrimaryGoal { get; set; }
    public Predicate<WorldState> MustRestGoal { get; set; }
    public EnergyPolicy EnergyPolicy { get; set; }

    public List<GoapAction> CurrentPlan { get; private set; } = [];
    private Predicate<WorldState>? currentGoal = null;
    private int planIndex = 0;


    public SimAgent(string id, Predicate<WorldState> goal, EnergyPolicy energyPolicy)
    {
        Id = id;
        PrimaryGoal = goal;
        EnergyPolicy = energyPolicy;

        MustRestGoal = (s) => s.Agents[Id].Energy >= EnergyPolicy.SatisfiedAbove;
    }

    public Predicate<WorldState> SelectGoal(WorldState state)
    {
        if (state.Agents[Id].Energy < EnergyPolicy.MustRestBelow)
            return MustRestGoal;
        else
            return PrimaryGoal;
    }

    public void Tick(WorldState world, List<GoapAction> availableActions)
    {
        var goal = SelectGoal(world);

        if (currentGoal != null && currentGoal(world))
        {
            Console.WriteLine($"Agent {Id}: Goal achieved!");
            CurrentPlan.Clear();
            currentGoal = null;
            return;
        }

        // Re-plan if needed
        if (CurrentPlan.Count == 0 || planIndex >= CurrentPlan.Count || currentGoal != goal)
        {
            Console.WriteLine($"Available actions for Agent {Id}:");
            foreach (var a in availableActions)
                Console.WriteLine(" * " + a);

            currentGoal = goal;
            CurrentPlan = Planner.Plan(world, Id, currentGoal, availableActions, debug: true);
            planIndex = 0;
            if (CurrentPlan.Count == 0)
            {
                Console.WriteLine($"Agent {Id}: No plan found for current goal.");
                return;
            }

            Console.WriteLine("Plan found:");
            foreach (var a in CurrentPlan)
                Console.WriteLine(" * " + a);
        }

        // Execute the next action
        var action = CurrentPlan[planIndex];

        // Check if action is still valid
        if (action.Precondition(world))
        {
            Console.WriteLine($"Agent {Id} executes: {action} [Energy: {world.Agents[Id].Energy}]");
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
