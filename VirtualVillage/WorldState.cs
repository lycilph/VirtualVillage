namespace VirtualVillage;

public class WorldState
{
    public int Wood = 0;
    public int Gold = 0;
    public bool HasAxe = false;
    public Location Location = new() { Name = "" };

    public WorldState Clone()
    {
        return new WorldState
        {
            Wood = Wood,
            Gold = Gold,
            HasAxe = HasAxe,
            Location = Location.Clone()
        };
    }

    public override string ToString() =>
        $"[Loc: {Location}, Wood: {Wood}, Gold: {Gold}, Axe: {HasAxe}]";
}