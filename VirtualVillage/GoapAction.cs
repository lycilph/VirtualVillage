namespace VirtualVillage;

public class GoapAction
{
    public string Name { get; }
    public Position TargetLocation { get; }
    public float BaseCost { get; }

    public List<StateRequirement> Preconditions { get; } = [];
    public List<ActionEffect> PredictedEffects { get; } = [];
    public Action<VillageWorld, Agent>? Execute { get; set; }

    public GoapAction(string name, Position location, float cost = 1f)
    {
        Name = name;
        TargetLocation = location;
        BaseCost = cost;
    }

    public float GetTotalCost(WorldState current)
    {
        // We look up current PosX and PosY to create a temporary Position object
        var currentPos = new Position(current.Get("PosX"), current.Get("PosY"));
        return BaseCost + currentPos.DistanceTo(TargetLocation);
    }
}
