using VirtualVillage.Actions;
using VirtualVillage.Core;

namespace VirtualVillage;

public class Villager
{
    public required string Name;
    public float Hunger = 100f; // Start full
    public WorldState CurrentState = [];
    public List<GoapAction> AvailableActions = [];

    // Store the current active plan
    public WorldState currentGoal = [];
    private Queue<GoapAction>? currentPlan = null;

    public void Update()
    {
        Console.WriteLine($"\n--- {Name}'s Turn ---");

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

        //// 1. If we don't have a plan, or the world changed and the plan is no longer valid, make a new one
        //if (currentPlan == null || currentPlan.Count == 0)
        //{
        //    currentPlan = SimplePlanner.Plan(CurrentState, Goal, AvailableActions, this);

        //    if (currentPlan.Count == 0)
        //    {
        //        Console.WriteLine($"{Name} is idling... No valid plan found.");
        //        return;
        //    }
        //}

        //// 2. Get the next action from the queue (without removing it yet)
        //var nextAction = currentPlan.Peek();

        //// 3. Check if preconditions are still met (in case the world changed)
        //if (!CurrentState.IsMet(nextAction.Preconditions))
        //{
        //    Console.WriteLine($"{Name}'s plan failed! Preconditions no longer met.");
        //    currentPlan = null; // Clear plan so we re-plan next tick
        //    return;
        //}

        //// 4. Execute and remove from queue
        //Console.WriteLine($"\n--- {Name}'s Turn ---");
        //nextAction.Execute(this);
        //currentPlan.Dequeue();

        //// 5. Apply effects to the villager's state
        //foreach (var effect in nextAction.Effects)
        //{
        //    CurrentState[effect.Key] = effect.Value;
        //}
    }
}