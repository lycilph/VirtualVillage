namespace VirtualVillage;

public class ConsoleGoapLogger : IGoapLogger
{
    public void Log(string message)
        => Console.WriteLine(message);
}
