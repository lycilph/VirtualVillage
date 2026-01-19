namespace VirtualVillage;


public static class Planner
{
    public class Node
    {
        public WorldState State;
        public GoapAction? Action;
        public Node? Parent;
        public float G; // Cost from start
        public float H;
        public float F;

        public Node(WorldState state, GoapAction? action, Node? parent, float g)
        {
            State = state;
            Action = action;
            Parent = parent;
            G = g;
        }
    }

    public static List<GoapAction> Plan(WorldState start, Predicate<WorldState> goal, List<GoapAction> actions, bool debug = true)
    {
        var openList = new List<Node> { new Node(start.Clone(), null, null, 0) };
        var closedList = new HashSet<string>();
        int iterations = 0;

        if (debug) Console.WriteLine("\n--- Starting Trace ---");

        while (openList.Count > 0)
        {
            iterations++;

            var current = openList.OrderBy(n => n.G).First();
            openList.Remove(current);
            
            if (debug)
            {
                string actionName = current.Action?.Name ?? "START";
                Console.WriteLine($"[Iter {iterations}] Expanding: {actionName} | State: {current.State} | Cost: {current.G}");
            }

            if (goal(current.State))
            {
                if (debug) Console.WriteLine($"Goal Found in {iterations} iterations!");
                return ReconstructPlan(current);
            }

            closedList.Add(current.State.ToString());

            foreach (var action in actions)
            {
                if (action.Precondition(current.State))
                {
                    var nextState = current.State.Clone();
                    action.Effect(nextState);

                    if (closedList.Contains(nextState.ToString()))
                    {
                        if (debug) Console.WriteLine($"  - Skipping {action.Name} (State already visited)");
                        continue;
                    }

                    if (debug) Console.WriteLine($"  + Adding Potential Action: {action.Name}"); 
                    openList.Add(new Node(nextState, action, current, current.G + action.Cost));
                }
            }
        }
        return new List<GoapAction>(); // No plan found
    }

    private static List<GoapAction> ReconstructPlan(Node node)
    {
        var plan = new List<GoapAction>();
        while (node?.Action != null)
        {
            plan.Insert(0, node.Action);
            node = node.Parent!;
        }
        return plan;
    }
}