using VirtualVillage.Actions;
using VirtualVillage.Core;
using VirtualVillage.Goals;

namespace VirtualVillage.Planning;

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
        Goal goal,
        IPlannerTracer? tracer = null)
    {
        var openSet = new PriorityQueue<Node, float>();
        var closedSet = new HashSet<WorldState>();
        var bestCosts = new Dictionary<WorldState, float>();

        var startNode = new Node(
            startState,
            Parent: null,
            Action: null,
            G: 0,
            F: Heuristic(startState, null, goal)
        );
        
        tracer?.Start(startState, goal);

        openSet.Enqueue(startNode, startNode.F);
        bestCosts[startState] = 0;

        const int max_nodes = 10000;
        var expanded_nodes = 0;

        while (openSet.Count > 0)
        {
            var current = openSet.Dequeue();

            tracer?.Expand(current.State, current.G, current.F);

            expanded_nodes++;
            if (expanded_nodes > max_nodes)
            {
                Console.WriteLine("Bailing out, infinite loop...");
                return null;
            }

            if (goal.DesiredState(current.State))
            {
                tracer?.GoalReached(current.State, expanded_nodes);
                var plan = ReconstructPlan(current);
                tracer?.Finished(plan);
                return plan;
            }

            if (!closedSet.Add(current.State))
            {
                tracer?.Skip("Already closed");
                continue;
            }

            foreach (var action in actions)
            {
                tracer?.ConsiderAction(action);

                if (!action.Precondition(current.State))
                {
                    tracer?.Skip("Precondition failed");
                    continue;
                }

                var nextState = current.State.Clone();
                var heuristic = Heuristic(nextState, action, goal); // Must be done before it is applied to the current state
                action.Effect(nextState);

                if (closedSet.Contains(nextState))
                    continue;

                float g = current.G + action.Cost;
                float f = g + heuristic;

                if (bestCosts.TryGetValue(nextState, out var best) && best <= g)
                {
                    tracer?.Skip($"Higher cost than best ({g} >= {best})");
                    continue;
                }
                bestCosts[nextState] = g;

                var node = new Node(
                    nextState,
                    current,
                    action,
                    g,
                    f
                );

                tracer?.Enqueue(nextState, g, f);
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

    private static List<GoapAction> ReconstructPlan(Node node, IPlannerTracer? tracer = null)
    {
        var plan = new List<GoapAction>();

        var current = node;
        while (current.Action != null)
        {
            plan.Add(current.Action);
            current = current.Parent!;
        }

        tracer?.PlanReconstructed(plan, node.F);

        plan.Reverse();
        return plan;
    }
}