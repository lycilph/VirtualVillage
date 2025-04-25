using VirtualVillageConsoleApp.Simulation;

namespace VirtualVillageConsoleApp.Goap;

public class GoapPlanner
{
    public List<GoapAction> GetPlan(Agent agent, GoapGoal goal, Dictionary<string, object> state, List<GoapAction> available_action, bool verbose = false)
    {
        var current_world_state = state.Clone();
        var root = new GoapNode(null, 0, current_world_state, null); // Root has no action

        // Build graph for achieving the goal
        var current_goal_state = goal.State.Clone();
        var leaves = new List<GoapNode>();
        BuildGraphRecursive(agent, root, current_goal_state, available_action, leaves);

        if (verbose)
        {
            foreach (var leaf in leaves)
            {
                var plan = UnpackPlan(leaf);
                Console.WriteLine($"Plan (cost {leaf.RunningCost}): {string.Join(',', plan.Select(a => a.Name))}");
            }
        }

        if (leaves.Count > 0)
        {
            var best_plan = leaves.OrderBy(n => n.RunningCost).First();
            return UnpackPlan(best_plan);
        }
        else
            return [];
    }

    // Recursive function to build the plan graph
    private void BuildGraphRecursive(Agent agent, GoapNode parent, Dictionary<string, object> goal_state, List<GoapAction> available_actions, List<GoapNode> leaves)
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
            parent.RunningCost += FindMovementCost(parent, agent);
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

            // Find movement cost here
            var move_cost = FindMovementCost(parent, action);

            var node = new GoapNode(parent, parent.RunningCost + action.Cost + move_cost, parent.State.Clone(), action);
            var remaining_action = available_actions.Except([action]).ToList();

            BuildGraphRecursive(agent, node, node_goal_state, remaining_action, leaves);
        }
    }

    private double FindMovementCost(GoapNode parent, GoapAction action)
    {
        if (parent.Action == null)
            return 0; // This is the case for the first action after the root node (so in reality where the agent ends up)
        else
        {
            var last_position = parent.Action!.Position;
            var current_position = action!.Position;
            return Position.Distance(last_position, current_position) / 10.0;
        }
    }

    private double FindMovementCost(GoapNode parent, Agent agent)
    {
        var last_position = parent.Action!.Position;
        var agent_position = agent.Position;
        return Position.Distance(last_position, agent_position) / 10.0;
    }

    private static List<GoapAction> UnpackPlan(GoapNode node)
    {
        var plan = new List<GoapAction>();
        var current_node = node;
        while (current_node.Parent != null)
        {
            if (current_node.Action != null)
                plan.Add(current_node.Action);
            current_node = current_node.Parent;
        }
        return plan;
    }
}
