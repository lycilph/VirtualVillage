namespace VirtualVillage;

public record AgentState(
    string Id,
    Location Location,
    Dictionary<string, int> Inventory
);