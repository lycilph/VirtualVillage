using VirtualVillage.Actions;

namespace VirtualVillage.Core;

public static class SimplePlanner
{
    private class Node(Node? parent, float cost, WorldState state, GoapAction? action)
    {
        public Node? Parent = parent;
        public float RunningCost = cost;
        public WorldState State = state;
        public GoapAction? Action = action;
    }

    public static Queue<GoapAction> Plan(WorldState startState, WorldState goal, List<GoapAction> availableActions, Villager agent)
    {
        List<Node> leaves = new List<Node>();
        Node root = new Node(null, 0, startState, null);

        if (BuildGraph(root, leaves, availableActions, goal, agent, 0))
        {
            // Find the cheapest leaf (lowest total cost)
            Node cheapest = leaves.OrderBy(n => n.RunningCost).First();

            // Reconstruct the path from leaf to root
            List<GoapAction> result = [];
            Node? current = cheapest;
            while (current != null && current.Action != null)
            {
                result.Insert(0, current.Action);
                current = current.Parent;
            }
            return new Queue<GoapAction>(result);
        }

        return new Queue<GoapAction>(); // No path found
    }

    private static bool BuildGraph(Node parent, List<Node> leaves, List<GoapAction> usableActions, WorldState goal, Villager agent, int depth)
    {
        if (depth > 10) return false; // Prevent infinite recursion/StackOverflow

        bool foundPath = false;
        foreach (var action in usableActions)
        {
            // Can we perform this action given the state at the parent node?
            if (parent.State.IsMet(action.Preconditions) && action.IsPossible(agent))
            {
                // Apply action effects to the parent's state to get the new state
                WorldState newState = parent.State.Clone();
                foreach (var effect in action.Effects)
                    newState[effect.Key] = effect.Value;

                Node node = new Node(parent, parent.RunningCost + action.Cost, newState, action);

                if (newState.IsMet(goal))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    if (BuildGraph(node, leaves, usableActions, goal, agent, depth+1))
                        foundPath = true;
                }
            }
        }
        return foundPath;
    }
}