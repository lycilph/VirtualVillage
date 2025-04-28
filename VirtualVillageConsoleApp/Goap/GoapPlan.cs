namespace VirtualVillageConsoleApp.Goap;

public class GoapPlan
{
    public double ActionCost { get; set; } = 0;
    public double MovementCost { get; set; } = 0;
    public List<GoapAction> Actions { get; set; } = [];

    public double TotalCost() => ActionCost + MovementCost;
}
