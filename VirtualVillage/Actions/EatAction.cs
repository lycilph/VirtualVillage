namespace VirtualVillage.Actions;

public class EatAction : GoapAction
{
    public EatAction() : base("Eat Bread")
    {
        Preconditions.Add("isHungry", true);
        Effects.Add("isHungry", false);
    }

    public override bool IsPossible(Villager agent) => Storehouse.Bread > 0;

    public override void Execute(Villager agent)
    {
        Storehouse.Bread--;
        agent.Hunger = 100f; // Fully fed
        agent.CurrentState["isHungry"] = false;
        Console.WriteLine($"{agent.Name} ate a loaf of bread.");
    }
}