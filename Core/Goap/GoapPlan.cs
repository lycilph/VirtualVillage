namespace Core.Goap;

public class GoapPlan<T> where T : GoapAction
{
    public double ActionCost { get; set; } = 0;
    public double MovementCost { get; set; } = 0;
    public List<T> Actions { get; set; } = [];

    public double TotalCost() => ActionCost + MovementCost;
}
