namespace VirtualVillage;

public class Planner
{
    private sealed record Node(
        WorldState State,
        Node? Parent,
        GoapAction? Action,
        float G,
        float F
    );

    public static List<GoapAction>? Plan(
        WorldState startState,
        List<GoapAction> actions,
        Goal goal)
    {
        var openSet = new PriorityQueue<Node, float>();
        var closedSet = new HashSet<WorldState>();

        var startNode = new Node(
            startState,
            Parent: null,
            Action: null,
            G: 0,
            F: Heuristic(startState, null, goal)
        );

        openSet.Enqueue(startNode, startNode.F);

        const int max_nodes = 10000;
        var expanded_nodes = 0;

        while (openSet.Count > 0)
        {
            var current = openSet.Dequeue();

            expanded_nodes++;
            if (expanded_nodes > max_nodes)
            {
                Console.WriteLine("Bailing out, infinite loop...");
                return null;
            }

            if (goal.DesiredState(current.State))
                return ReconstructPlan(current);

            closedSet.Add(current.State);

            foreach (var action in actions)
            {
                if (!action.Precondition(current.State))
                    continue;

                var nextState = current.State.Clone();
                var heuristic = Heuristic(nextState, action, goal); // Must be done before it is applied to the current state
                action.Effect(nextState);

                if (closedSet.Contains(nextState))
                    continue;

                float g = current.G + action.Cost;
                float f = g + heuristic;

                var node = new Node(
                    nextState,
                    current,
                    action,
                    g,
                    f
                );

                openSet.Enqueue(node, node.F);
            }
        }

        return null; // No plan found
    }

    private static float Heuristic(WorldState state, GoapAction? action, Goal goal)
    {
        if (action == null || action.Entity == null)
            return 0;

        var action_location = action.Entity.Location;
        var agent_location = state.Get<Location>("agent_location");
        return agent_location.DistanceTo(action_location);
    }

    private static List<GoapAction> ReconstructPlan(Node node)
    {
        var plan = new List<GoapAction>();

        var current = node;
        while (current.Action != null)
        {
            plan.Add(current.Action);
            current = current.Parent!;
        }

        plan.Reverse();
        return plan;
    }
}