using Core.Goap;
using VirtualVillage.Actions;
using VirtualVillage.Objects;

namespace VirtualVillage;

public class Villager : GoapAgent<ActionBase>, IWorldObject
{
    public Dictionary<string, int> Inventory { get; set; } = [];

    public void AddWood(int amount)
    {
        if (!Inventory.TryAdd(World.Wood, amount))
            Inventory[World.Wood] += amount;
    }

    public void Update(World world)
    {
        if (CurrentPlan == null)
        {
            CreateNewPlan(world);
        }
    }

    private void CreateNewPlan(World world)
    {
        // Update world state
        var storehouse = world.Get<Storehouse>();
        var world_state = new Dictionary<string, object>();
        if (storehouse.Has(World.Tools))
            world_state.Add("StoredTools", true);

        // Update actions
        foreach (var action in Actions)
            action.Update(world, this);

        // Find goal
        var goal = Goals.First();

        // Run planner
        CurrentPlan = GoapPlanner<ActionBase>.GetBestPlan(Position, goal, world_state, Actions, 10);
        CurrentAction = CurrentPlan?.Actions.FirstOrDefault();
    }

    public void Render()
    {
        Console.WriteLine($"Villager {Name} - {Position}");
        foreach (var item in Inventory)
            Console.WriteLine($"  {item}");
        if (CurrentPlan != null)
            Console.WriteLine($"  Plan: {string.Join(',', CurrentPlan.Actions.Select(a => a.Name))}");
        if (CurrentAction != null)
            Console.WriteLine($"  Action: {CurrentAction.Name}");
    }
}
