namespace VirtualVillage.Actions;

public sealed class ExecutionContext(GoapAction action)
{
    public GoapAction Action { get; } = action;
    public float Elapsed { get; private set; } = 0f;

    public void Tick() => Elapsed += 1;
}
