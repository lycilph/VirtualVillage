namespace VirtualVillage;

public class PositionDistanceHeuristic : IGoapHeuristic
{
    public int Estimate(GoapState current, GoapState goal)
    {
        // If goal explicitly specifies a position
        if (goal.TryGet<Position>("Position", out var goalPos) &&
            current.TryGet<Position>("Position", out var currentPos))
        {
            return currentPos.DistanceTo(goalPos);
        }

        // Otherwise: no positional hint
        return 0;
    }
}
