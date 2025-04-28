using Core;
using VirtualVillageConsoleApp.Simulation;

namespace VirtualVillageConsoleApp.Goap;

public class GoapPlanner
{
    public GoapPlan GetPlan(Agent agent, GoapGoal goal, Dictionary<string, object> state, List<GoapAction> available_action, bool verbose = false)
    {
        var current_world_state = state.Clone();
        var root = new GoapNode(null, 0, current_world_state, null); // Root has no action

        // Build graph for achieving the goal
        var current_goal_state = goal.State.Clone();
        var leaves = new List<GoapNode>();
        BuildGraphRecursive(root, current_goal_state, available_action, leaves);

        var plans = leaves
            .Select(UnpackPlan)
            .Select(p => EvaluateMovementCost(agent, p))
            .OrderBy(p => p.TotalCost())
            .ToList();

        if (verbose)
        {
            foreach (var plan in plans)
                Console.WriteLine($"Plan (Actions {plan.ActionCost}, Movement {plan.MovementCost}, Total {plan.TotalCost()}): {string.Join(',', plan.Actions.Select(a => a.Name))}");
        }

        return plans.FirstOrDefault() ?? new();
    }

    // Recursive function to build the plan graph
    private void BuildGraphRecursive(GoapNode parent, Dictionary<string, object> goal_state, List<GoapAction> available_actions, List<GoapNode> leaves)
    {
        //Console.WriteLine("-------------------------------------------------------------");
        //Console.WriteLine("Current state:");
        //foreach (var kvp in parent.State)
        //    Console.WriteLine($"{kvp.Key} - {kvp.Value}");
        //Console.WriteLine("Goal state:");
        //foreach (var kvp in goal_state)
        //    Console.WriteLine($"{kvp.Key} - {kvp.Value}");
        //Console.WriteLine();

        // Check if the parent state already satisfies the goal
        if (goal_state.Count == 0)
        {
            //Console.WriteLine("Goal satisfied!");
            leaves.Add(parent);
            return;
        }

        // The node_goal_state now only contains unfulfilled conditions, loop over available actions to see if any helps with current goal
        foreach (var action in available_actions)
        {
            // Check if the action's effects satisfy *any* of the currently remaining goal conditions
            if (!action.Effects.Keys.Any(key => goal_state.ContainsKey(key)))
            {
                //Console.WriteLine($"Action {action.Name} skipped");
                continue; // This action doesn't help with the *current* remaining goal conditions
            }

            //Console.WriteLine($"Processing action {action.Name}");
        
            var node_goal_state = goal_state.Clone();

            // Remove all action effects from goal state
            foreach (var kvp in action.Effects)
                node_goal_state.Remove(kvp.Key);
            // Add all action preconditions to goal state
            foreach (var kvp in action.Preconditions)
                node_goal_state.Add(kvp.Key, kvp.Value);

            // Check if parent node state satisfies any conditions of the goal state
            foreach (var kvp in parent.State)
                node_goal_state.Remove(kvp.Key);

            var node = new GoapNode(parent, parent.RunningCost + action.Cost, parent.State.Clone(), action);
            var remaining_action = available_actions.Except([action]).ToList();

            BuildGraphRecursive(node, node_goal_state, remaining_action, leaves);
        }
    }

    private static GoapPlan UnpackPlan(GoapNode node)
    {
        var plan = new GoapPlan() { ActionCost = node.RunningCost };
        var current_node = node;
        while (current_node.Parent != null)
        {
            if (current_node.Action != null)
                plan.Actions.Add(current_node.Action);
            current_node = current_node.Parent;
        }
        return plan;
    }

    private static GoapPlan EvaluateMovementCost(Agent agent, GoapPlan plan)
    {
        plan.MovementCost = Position.Distance(agent.Position, plan.Actions.First().Position);
        for (var i = 0; i < plan.Actions.Count-1; i++)
            plan.MovementCost += Position.Distance(plan.Actions[i].Position, plan.Actions[i+1].Position);
        plan.MovementCost /= 10.0;
        return plan;
    }
}
