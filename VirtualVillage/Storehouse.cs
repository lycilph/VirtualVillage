namespace VirtualVillage;

public static class Storehouse
{
    // Common resources
    public static int Axes = 1;
    public static int Firewood = 0;
    public static int Bread = 5;

    public static void PrintStatus()
    {
        Console.WriteLine($"[Storehouse] Axes: {Axes}, Wood: {Firewood}, Bread: {Bread}");
    }
}