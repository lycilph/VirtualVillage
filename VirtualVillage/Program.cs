using Core.Goap;

namespace VirtualVillage;

internal class Program
{
    static void Main()
    {
        var agent = new GoapAgent(){ Name = "Test", Position = new System.Numerics.Vector2(25, 12) };

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
