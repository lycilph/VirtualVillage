using System.Numerics;

namespace Core.Goap;

public static class GoapPlanner<T> where T : GoapAction
{
    public static GoapPlan? GetBestPlan(Vector2 start_position, GoapGoal goal, Dictionary<string, object> world_state, List<T> available_action, double movement_cost_weight) 
    {
        var plans = GetPlans(goal, world_state, available_action);
        
        return plans
            .Select(p => EvaluateMovementCost(p, start_position, movement_cost_weight))
            .OrderBy(p => p.TotalCost())
            .FirstOrDefault();
    }

    public static List<GoapPlan> GetPlans(GoapGoal goal, Dictionary<string, object> world_state, List<T> available_action)
    {
        var current_world_state = world_state.Clone();
        var root = new GoapNode(null, 0, current_world_state, null); // Root has no action

        // Build graph for achieving the goal
        var current_goal_state = goal.State.Clone();
        var leaves = new List<GoapNode>();
        BuildGraphRecursive(root, current_goal_state, available_action, leaves);

        return leaves
            .Select(UnpackPlan)
            .OrderBy(p => p.TotalCost())
            .ToList();
    }

    // Recursive function to build the plan graph
    private static void BuildGraphRecursive(GoapNode parent, Dictionary<string, object> goal_state, List<T> available_actions, List<GoapNode> leaves)
    {
        // Check if the parent state already satisfies the goal
        if (goal_state.Count == 0)
        {
            leaves.Add(parent);
            return;
        }

        // The node_goal_state now only contains unfulfilled conditions, loop over available actions to see if any helps with current goal
        foreach (var action in available_actions)
        {
            // Check if the action's effects satisfy *any* of the currently remaining goal conditions
            if (!action.Effects.Keys.Any(key => goal_state.ContainsKey(key)))
                continue; // This action doesn't help with the *current* remaining goal conditions

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

    private static GoapPlan EvaluateMovementCost(GoapPlan plan, Vector2 start_position, double movement_cost_weight)
    {
        plan.MovementCost = Vector2.Distance(start_position, plan.Actions.First().Position);
        for (var i = 0; i < plan.Actions.Count - 1; i++)
            plan.MovementCost += Vector2.Distance(plan.Actions[i].Position, plan.Actions[i + 1].Position);
        plan.MovementCost /= movement_cost_weight;
        return plan;
    }
}
