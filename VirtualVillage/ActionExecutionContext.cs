namespace VirtualVillage;

public sealed class ActionExecutionContext
{
    public GoapState PreState { get; }

    public ActionExecutionContext(GoapState preState)
    {
        PreState = preState;
    }
}