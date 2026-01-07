namespace VirtualVillage;

public class Agent
{
    public string Name { get; set; }
    public WorldState State { get; set; } = new WorldState();

    private Queue<GoapAction> plan = new();
    private GoapAction? currentAction;

    public Agent(string name, Position startPos)
    {
        Name = name;
        State.Set("PosX", startPos.X);
        State.Set("PosY", startPos.Y);
    }

    // Helper to get current position as a Position object
    public Position CurrentPosition => new(State.Get("PosX"), State.Get("PosY"));

    public void Update(List<GoapAction> worldActions)
    {
        // 1. Planning Phase
        if (currentAction == null && plan.Count == 0)
        {
            // For now, a simple hardcoded goal to test the system
            var goals = new List<StateRequirement> {
                new StateRequirement { Key = "Logs", Value = 2, Condition = ConditionType.GreaterThan }
            };

            plan = new Planner().Plan(State, worldActions, goals);
            return;
        }

        // 2. Fetch next task from plan
        if (currentAction == null && plan.Count > 0)
        {
            currentAction = plan.Dequeue();
            Console.WriteLine($"{Name} started task: {currentAction.Name}");
        }

        // 3. Execution Phase
        if (currentAction != null)
        {
            if (CurrentPosition.Equals(currentAction.TargetLocation))
            {
                PerformAction();
            }
            else
            {
                MoveToward(currentAction.TargetLocation);
            }
        }
    }

    private void MoveToward(Position target)
    {
        int curX = State.Get("PosX");
        int curY = State.Get("PosY");

        // Move one step at a time
        if (curX < target.X) curX++; else if (curX > target.X) curX--;
        if (curY < target.Y) curY++; else if (curY > target.Y) curY--;

        State.Set("PosX", curX);
        State.Set("PosY", curY);

        Console.WriteLine($"{Name} moving: {CurrentPosition} -> {target}");
    }

    private void PerformAction()
    {
        if (currentAction == null) return;

        Console.WriteLine($"[ACTION] {Name} is performing {currentAction.Name} at {CurrentPosition}");

        foreach (var effect in currentAction.Effects)
        {
            effect.Apply(State);
        }

        currentAction = null; // Mark as finished
    }
}