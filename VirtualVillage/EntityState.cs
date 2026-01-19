namespace VirtualVillage;

public record EntityState(
    string Id,
    string Kind,
    Location Location,
    Dictionary<string, int> Resources
);
