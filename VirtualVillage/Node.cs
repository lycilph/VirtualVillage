namespace VirtualVillage;

public class Node
{
    public WorldState State;
    public float G; // This is the actual cost so far
    public GoapAction? Action;
    public Node? Parent;

    public Node(WorldState s, float g, GoapAction? a, Node? p) 
    {
        State = s;
        G = g; 
        Action = a; 
        Parent = p; 
    }
}
