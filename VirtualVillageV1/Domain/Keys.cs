namespace VirtualVillage.Domain;

public static class Keys
{
    // Common
    public const string Location = "location";
    public const string Relax = "relax";

    // Resources
    public const string Wood = "wood";
    public const string Ore = "ore";

    // Tools
    public const string Axe = "axe";
    public const string Pickaxe = "pickaxe";

    // Jobs
    public const string Lumberjack = "lumberjack";
    public const string Miner = "miner";
    public const string Blacksmith = "blacksmith";
    public const string AllJobs = "all";

    // Items (ie. resources and tools)
    public static List<string> Items = [Wood, Ore, Axe, Pickaxe];
}
