namespace VirtualVillage;

class Program
{
    static void Main()
    {
        int iteration = 0;
        while (true)
        {
            Console.WriteLine($"=== Village Status (iteration {iteration})===");
            iteration++;

            Console.WriteLine();
            Console.Write("Step simulation [any key] or Quit [q]");
            var keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Q)
                break;
        }
    }
}
