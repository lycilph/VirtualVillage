namespace VirtualVillage;

public interface IGoapHeuristic
{
    int Estimate(GoapState current, GoapState goal);
}
