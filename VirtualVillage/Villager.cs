using VirtualVillage.Actions;
using VirtualVillage.Core;

namespace VirtualVillage;

public class Villager
{
    public required string Name;
    public Location CurrentLocation = Location.Home;
    public float Hunger = 100f; // Start full
    public WorldState CurrentState = [];
    public List<GoapAction> AvailableActions = [];

    // Store the current active plan
    public WorldState currentGoal = [];
    private Queue<GoapAction>? currentPlan = null;

    public void Update()
    {
        Console.WriteLine($"\n--- {Name}'s Turn ---");

        // Sync Enum to WorldState Dictionary
        CurrentState["atStorehouse"] = (CurrentLocation == Location.Storehouse);
        CurrentState["atWoods"] = (CurrentLocation == Location.Woods);
        CurrentState["atHome"] = (CurrentLocation == Location.Home);

        // 1. Hunger decreases every turn
        Hunger -= 15f;
        CurrentState["isHungry"] = (Hunger < 30);

        // 2. Dynamic Goal Selection
        WorldState newGoal = new WorldState();
        if (CurrentState["isHungry"])
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
            currentPlan = SimplePlanner.Plan(CurrentState, currentGoal, AvailableActions, this);

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
            if (CurrentState.IsMet(nextAction.Preconditions) && nextAction.IsPossible(this))
            {
                Console.WriteLine($"{Name} (Hunger: {Hunger:0}): {nextAction.Name}");
                nextAction.Execute(this);
                currentPlan.Dequeue();

                // 5. Apply effects to the villager's state
                foreach (var effect in nextAction.Effects)
                {
                    CurrentState[effect.Key] = effect.Value;
                }
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