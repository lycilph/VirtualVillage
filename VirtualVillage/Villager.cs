using System.Numerics;
using Core.Goap;
using VirtualVillage.Actions;
using VirtualVillage.Objects;

namespace VirtualVillage;

public class Villager : GoapAgent<ActionBase>, IWorldObject
{
    public Dictionary<string, int> Inventory { get; set; } = [];

    public void Update(World world)
    {
        if (CurrentPlan == null)
            CreateNewPlan(world);

        if (CurrentAction != null)
        {
            // Check if the villager is at the target of the action
            var dist = Vector2.Distance(Position, CurrentAction.Position);
            if (dist < 1)
            {
                Console.WriteLine($"Villager {Name} performing action {CurrentAction.Name}");
                var result = CurrentAction.Perform(world, this);
                if (result == ActionBase.ActionResult.Completed)
                    NextAction();
                else
                    CurrentPlan = null; // Cancel current plan and create a new next update
            }
            else
            {
                // Move towards the action position
                var dir = CurrentAction.Position - Position;
                dir = Vector2.Divide(dir, dir.Length());
                Position += dir;
            }
        }
    }

    private void NextAction()
    {
        if (CurrentPlan == null || CurrentAction == null)
            return;

        CurrentPlan.Actions.Remove(CurrentAction);

        if (CurrentPlan.Actions.Count > 0)
            CurrentAction = CurrentPlan.Actions.First();
        else
            CurrentPlan = null; // Current plan is completed, so null to signal that a new needs to be created next update
    }

    private void CreateNewPlan(World world)
    {
        // Update world state
        var world_state = new Dictionary<string, object>();

        var storehouse = world.Get<Storehouse>();
        if (storehouse.Inventory.Has(World.Tools))
            world_state.Add($"Stored{World.Tools}", true);

        if (Inventory.Has(World.Tools))
            world_state.Add($"Has{World.Tools}", true);

        // Update actions
        foreach (var action in Actions)
            action.Update(world, this);

        // Find goal
        var goal = Goals.First();

        // Run planner
        CurrentPlan = GoapPlanner<ActionBase>.GetBestPlan(Position, goal, world_state, Actions, 10);

        if (CurrentPlan != null && CurrentPlan.Actions.Count > 0)
            CurrentAction = CurrentPlan.Actions.First();
    }

    public void Render()
    {
        Console.WriteLine($"Villager {Name} - {Position}");
        foreach (var item in Inventory)
            Console.WriteLine($"  {item}");
        if (CurrentPlan != null)
            Console.WriteLine($"  Plan: {string.Join(',', CurrentPlan.Actions.Select(a => a.Name))}");
        if (CurrentAction != null)
            Console.WriteLine($"  Action: {CurrentAction.Name} - {CurrentAction.Position}");
    }
}
