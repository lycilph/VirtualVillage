using System.Numerics;
using VirtualVillage.Objects;

namespace VirtualVillage;

public class World
{
    public const string Tools = "Tools";
    public const string Wood = "Wood";

    private const int seed = 12345;
    private readonly Random rnd = new Random(seed);

    public int Size { get; set; } = 100;

    public List<Villager> Villagers { get; set; } = [];
    public List<IWorldObject> WorldObjects { get; set; } = [];

    public T Get<T>() where T : class
    {
        return WorldObjects.Single(obj => obj is T) as T ?? throw new Exception($"Couldn't find a {typeof(T)}");
    }

    public T GetClosest<T>(Vector2 pos) where T : IWorldObject
    {
        return WorldObjects
            .Where(obj => obj is T)
            .Cast<T>()
            .Select(obj => new { Object = obj, Distance = Vector2.Distance(obj.Position, pos) })
            .OrderBy(p => p.Distance)
            .First()
            .Object ?? throw new Exception($"Couldn't find a {typeof(T)}");
    }

    public Vector2 GetRandomPosition() => new Vector2(rnd.Next(0, Size), rnd.Next(0, Size));

    public void Update() 
    {
        Console.WriteLine("Updating world:");

        foreach (var villager in Villagers)
            villager.Update(this);
        foreach (var obj in WorldObjects)
            obj.Update(this);
    }
 
    public void Render() 
    {
        Console.WriteLine();

        foreach (var villager in Villagers)
            villager.Render();
        foreach (var obj in WorldObjects)
            obj.Render();
    }
}
