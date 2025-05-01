namespace VirtualVillage;

internal class Program
{
    static void Main()
    {
        var world = WorldFactory.CreateWorld();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Step simulation");
            Console.WriteLine("2. Quite");
            Console.WriteLine("Choice: ");
            Console.WriteLine();

            world.Update();
            world.Render();
            
            var key = Console.ReadKey();
            if (key.KeyChar == '2')
                break;
        }
    }
}
