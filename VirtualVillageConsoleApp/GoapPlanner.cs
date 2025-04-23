
using VirtualVillageConsoleApp.Actions;

namespace VirtualVillageConsoleApp;

public class GoapPlanner
{
    public Queue<GoapAction> CreatePlan()
    {
        var plan = new Queue<GoapAction>();

        plan.Enqueue(new IdleAction());

        return plan;
    }
}
