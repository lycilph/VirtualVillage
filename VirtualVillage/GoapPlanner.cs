namespace VirtualVillage;

public class GoapPlanner
{
    private readonly IGoapLogger? logger;
    private readonly IGoapHeuristic? heuristic;

    public GoapPlanner(
        IGoapLogger? logger = null,
        IGoapHeuristic? heuristic = null)
    {
        this.logger = logger;
        this.heuristic = heuristic;
    }

    private class Node
    {
        public GoapState State = null!;
        public Node? Parent;
        public GoapAction? Action;
        public int Cost;          // g
        public int EstimatedCost; // f = g + h
        public int Depth;
    }

    public List<GoapAction>? Plan(
        GoapState start,
        GoapState goal,
        List<GoapAction> actions)
    {
        logger?.Log("=== GOAP PLANNING START ===");
        logger?.Log($"Start: {start.ToDebugString()}");
        logger?.Log($"Goal : {goal.ToDebugString()}");

        var open = new List<Node>
        {
            new Node
            {
                State = start,
                Cost = 0,
                Depth = 0
            }
        };

        var closed = new HashSet<GoapState>();

        while (open.Any())
        {
            // Pick cheapest node (Dijkstra-style)
            var current = open.OrderBy(n => n.EstimatedCost).First();
            open.Remove(current);

            // Skip already-visited states
            if (closed.Contains(current.State))
                continue;

            logger?.Log(
                $"Expand (cost={current.Cost}, depth={current.Depth}) " +
                $"{current.State.ToDebugString()}");

            // Goal test
            if (current.State.Satisfies(goal))
            {
                logger?.Log("GOAL SATISFIED");
                var plan = BuildPlan(current);
                LogPlan(plan);
                return plan;
            }

            closed.Add(current.State);

            foreach (var action in actions)
            {
                if (!action.CanRun(current.State))
                    continue;

                var newState = current.State.Clone();
                action.Apply(newState);

                // 🚫 Skip actions that don't change state
                if (newState.Equals(current.State))
                    continue;

                // 🚫 Skip already explored states
                if (closed.Contains(newState))
                    continue;

                var newCost = current.Cost + action.GetCost(current.State);

                var g = current.Cost + action.GetCost(current.State);
                var h = heuristic?.Estimate(newState, goal) ?? 0;

                logger?.Log(
                    $"  + {action.Name,-15} -> g={g,-3} h={h,-3} f={g + h,-3} | {newState.ToDebugString()}");

                open.Add(new Node
                {
                    State = newState,
                    Parent = current,
                    Action = action,
                    Cost = g,
                    EstimatedCost = g + h,
                    Depth = current.Depth + 1
                });
            }
        }

        logger?.Log("NO PLAN FOUND");
        return null;
    }

    private List<GoapAction> BuildPlan(Node node)
    {
        var result = new List<GoapAction>();
        while (node.Action != null)
        {
            result.Insert(0, node.Action);
            node = node.Parent!;
        }
        return result;
    }

    private void LogPlan(List<GoapAction> plan)
    {
        logger?.Log("=== PLAN RESULT ===");
        if (!plan.Any())
        {
            logger?.Log("(empty plan)");
            return;
        }

        for (int i = 0; i < plan.Count; i++)
            logger?.Log($"{i + 1}. {plan[i].Name}");
    }
}