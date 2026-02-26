namespace VirtualVillageConsole.Misc;

public sealed class IdGenerator
{
    private static readonly Lazy<IdGenerator> instance = new(() => new IdGenerator());

    private int currentId = 0;

    private IdGenerator() { }

    public static IdGenerator Instance => instance.Value;

    // Thread-safe
    public int GetNextId() => Interlocked.Increment(ref currentId);

    public static int Next() => Instance.GetNextId();
}