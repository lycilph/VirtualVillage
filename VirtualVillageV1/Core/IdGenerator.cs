namespace VirtualVillage.Core;

public sealed class IdGenerator
{
    private static readonly Lazy<IdGenerator> instance = new(() => new IdGenerator());

    private int currentId = 0;

    private IdGenerator() { }

    public static IdGenerator Instance => instance.Value;

    public int GetNextId()
    {
        // Thread-safe
        return Interlocked.Increment(ref currentId);
    }

    public static int Next() => Instance.GetNextId();
}