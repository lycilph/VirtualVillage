namespace VirtualVillage;

public class Planner
{
    public Queue<GoapAction> Plan(WorldState start, List<GoapAction> available, List<StateRequirement> goals)
    {
        var open = new List<Node> { new Node(start.Clone(), 0, null, null) };
        var closed = new List<Node>();

        while (open.Count > 0)
        {
            var curr = open.OrderBy(n => n.G).First();
            open.Remove(curr);

            if (goals.All(g => g.IsMet(curr.State)))
                return Reconstruct(curr);

            closed.Add(curr);

            foreach (var act in available)
            {
                if (act.Preconditions.All(p => p.IsMet(curr.State)))
                {
                    var nextS = curr.State.Clone();
                    foreach (var e in act.PredictedEffects) e.Apply(nextS);

                    var newNode = new Node(nextS, curr.G + act.GetTotalCost(curr.State), act, curr);

                    // Improved state comparison to avoid redundant nodes
                    if (!closed.Any(c => StatesMatch(c.State, newNode.State)))
                        open.Add(newNode);
                }
            }
        }
        return new Queue<GoapAction>(); // Return empty rather than null
    }

    private bool StatesMatch(WorldState a, WorldState b)
    {
        return a.Values.Count == b.Values.Count && !a.Values.Except(b.Values).Any();
    }

    private Queue<GoapAction> Reconstruct(Node goal)
    {
        var p = new List<GoapAction>();
        Node? curr = goal; // Use nullable Node for the traversal
        while (curr != null && curr.Action != null)
        {
            p.Add(curr.Action);
            curr = curr.Parent;
        }
        p.Reverse();
        return new Queue<GoapAction>(p);
    }
}
