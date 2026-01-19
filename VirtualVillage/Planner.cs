namespace VirtualVillage;


public static class Planner
{
    public static List<GoapAction> Plan(
       WorldState start,
       string agentId,
       Predicate<WorldState> goal,
       IEnumerable<GoapAction> actions,
       bool debug = false,
       int maxIterations = 1000)
    {
        var open = new PriorityQueue<PlanNode, float>();
        var closed = new HashSet<int>();

        var startNode = new PlanNode(
            state: start.Clone(),
            parent: null,
            action: null,
            g: 0,
            h: GoapHeuristics.DistanceToRelevantEntity(start, agentId, goal)
        );
        open.Enqueue(startNode, startNode.F);

        int iterations = 0;

        while (open.Count > 0 && iterations++ < maxIterations)
        {
            var current = open.Dequeue();
            var key = WorldStateKey.ComputeHash(current.State);

            if (closed.Contains(key))
                continue;

            if (debug)
            {
                Console.WriteLine(
                    $"[{iterations}] G={current.G:0.0} H={current.H:0.0} F={current.F:0.0} " +
                    $"Action={current.Action?.ToString() ?? "START"}");
            }

            if (goal(current.State))
                return ReconstructPlan(current);

            closed.Add(key);

            foreach (var action in actions)
            {
                if (!action.Precondition(current.State))
                    continue;

                var nextState = current.State.Clone();
                action.Effect(nextState);

                var nextKey = WorldStateKey.ComputeHash(nextState);
                if (closed.Contains(nextKey))
                    continue;

                var g = current.G + action.Cost;
                var h = GoapHeuristics.DistanceToRelevantEntity(
                    nextState, agentId, goal);

                var node = new PlanNode(
                    state: nextState,
                    parent: current,
                    action: action,
                    g: g,
                    h: h
                );

                open.Enqueue(node, node.F);
            }
        }

        return new List<GoapAction>();
    }

    private static List<GoapAction> ReconstructPlan(PlanNode node)
    {
        var plan = new List<GoapAction>();

        while (node.Action != null)
        {
            plan.Insert(0, node.Action);
            node = node.Parent!;
        }

        return plan;
    }
}