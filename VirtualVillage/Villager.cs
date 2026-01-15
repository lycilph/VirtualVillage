using VirtualVillage.Actions;
using VirtualVillage.Core;

namespace VirtualVillage;

public class Villager
{
    public required string Name;
    public Location CurrentLocation = Location.Home;
    public float Hunger = 100f;
    public bool HasAxe = false;
    public bool HasFirewood = false;

    public List<GoapAction> AvailableActions = [];

    // Store the current active plan
    public WorldState currentGoal = [];
    private Queue<GoapAction>? currentPlan = null;
    
    public WorldState GetSnapshot()
    {
        var state = new WorldState
        {
            ["atStorehouse"] = (CurrentLocation == Location.Storehouse),
            ["atWoods"] = (CurrentLocation == Location.Woods),
            ["atHome"] = (CurrentLocation == Location.Home),
            ["isHungry"] = (Hunger < 30),
            ["hasAxe"] = HasAxe,
            ["hasFirewood"] = HasFirewood
        };
        return state;
    }

    public void Update()
    {
        Console.WriteLine($"\n--- {Name}'s Turn ---");

        // 1. Hunger decreases every turn
        Hunger -= 15f;
        var currentSnapshot = GetSnapshot();

        // 2. Dynamic Goal Selection
        WorldState newGoal = new WorldState();
        if (currentSnapshot["isHungry"])
        {
            newGoal.Add("isHungry", false); // Priority: Eat!
        }
        else
        {
            newGoal.Add("deliveredWood", true); // Default: Work
        }

        // 3. Re-plan if goal changed or plan finished
        if (currentGoal == null || !currentGoal.IsMet(newGoal) || currentPlan == null || currentPlan.Count == 0)
        {
            currentGoal = newGoal;
            currentPlan = SimplePlanner.Plan(currentSnapshot, currentGoal, AvailableActions, this);

            if (currentPlan.Count == 0)
            {
                Console.WriteLine($"{Name} is idling... No valid plan found.");
                return;
            }
        }

        // 4. Execution logic (same as before)
        if (currentPlan != null && currentPlan.Count > 0)
        {
            var nextAction = currentPlan.Peek();
            if (currentSnapshot.IsMet(nextAction.Preconditions) && nextAction.IsPossible(this))
            {
                Console.WriteLine($"{Name} (Hunger: {Hunger:0}): {nextAction.Name}");
                nextAction.Execute(this);
                currentPlan.Dequeue();
            }
            else
            {
                Console.WriteLine($"{Name}'s plan failed! Preconditions no longer met.");
                currentPlan = null; // Forces re-plan next turn
                return;
            }
        }
    }
}