namespace VirtualVillageConsoleApp;

public class Item
{
    public string Name { get; set; } = string.Empty;
    public Position Position { get; set; } = new Position();

    public void Render()
    {
        Console.WriteLine($"Item {Name} {Position}");
    }
}
