using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using Core;
using Core.Goap;

namespace VirtualVillage;

public partial class MainWindow : Window
{
    private World world = new();
    private Agent lumberjack = new Agent(new Position(5, 5), Brushes.Blue, "Lumberjack");
    private DispatcherTimer timer = new();
    private ICollectionView view;

    public ObservableCollection<IWorldObject> WorldObjects
    {
        get { return (ObservableCollection<IWorldObject>)GetValue(WorldObjectsProperty); }
        set { SetValue(WorldObjectsProperty, value); }
    }
    public static readonly DependencyProperty WorldObjectsProperty =
        DependencyProperty.Register(nameof(WorldObjects), typeof(ObservableCollection<IWorldObject>), typeof(MainWindow), new PropertyMetadata(null));

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        world.Store.Position = new Position(250, 250);
        world.Store.Inventory.Add(InventoryObjects.Food, 5);
        world.Store.Inventory.Add(InventoryObjects.Tools, 1);

        lumberjack.Actions =
            [
                new GoapAction
                {
                    Name = "Chop Wood",
                    Cost = 4,
                    Position = new(5, 5), // Should be update to the nearest tree
                    Preconditions = { { "HasTool", true } },
                    Effects = { { "HasWood", true } },
                },
                new GoapAction
                {
                    Name = "Find Twigs",
                    Cost = 8,
                    Position = new(37, 12), // This should be updated to a random position each time the planner is run
                    Action = () => lumberjack.Inventory.Add(InventoryObjects.Woold, 1),
                    Effects = { { "HasWood", true } },
                },
                new GoapAction
                {
                    Name = "Store Resource [Wood]",
                    Cost = 1,
                    Position = world.Store.Position,
                    Action = () => lumberjack.Inventory[InventoryObjects.Woold] -= 1,
                    Preconditions = { { "HasWood", true } },
                    Effects = { { "StoredWood", true } },
                },
                new GoapAction
                {
                    Name = "Get Resource [Tool]",
                    Cost = 1,
                    Position = world.Store.Position,
                    Preconditions = { { "StoredTool", true } },
                    Effects = { { "HasTool", true } },
                }
            ];
        lumberjack.Goals =
            [
                new()
                {
                    Name = "Gather Wood",
                    State = { { "StoredWood", true } }
                }
            ];

        world.Agents =
            [
                lumberjack,
                new Agent(new Position(250, 15), Brushes.Yellow, "Miner"),
                new Agent(new Position(450, 40), Brushes.Red, "Farmer"),
                new Agent(new Position(120, 150), Brushes.Violet, "Blacksmith")
            ];

        world.Trees =
            [
                new Tree(new Position(375, 12), Brushes.Green, 3),
                new Tree(new Position(75, 312), Brushes.Green, 3),
                new Tree(new Position(75, 12), Brushes.Green, 3),
            ];

        WorldObjects = new ObservableCollection<IWorldObject>(world.GetWorldObjects());
        view = CollectionViewSource.GetDefaultView(WorldObjects);

        timer.Tick += TimerTick;
        timer.Interval = TimeSpan.FromMilliseconds(200);
    }

    private void TimerTick(object? sender, EventArgs e)
    {
        //lumberjack.Position.X += 10;
        //if (lumberjack.Position.X >= 500)
        //    lumberjack.Position.X = 5;

        // Check to see if the agent has any actions to perform
        if (lumberjack.CurrentAction == null)
        {
            Dictionary<string, object> world_state = new() { { "StoredTool", true } };
            lumberjack.CurrentPlan = GoapPlanner.GetBestPlan(lumberjack.Position, lumberjack.Goals.First(), world_state, lumberjack.Actions, 25);
            lumberjack.CurrentAction = lumberjack.CurrentPlan?.Actions.FirstOrDefault();
            lumberjack.CurrentPlan!.Actions.Remove(lumberjack.CurrentAction!);
        }

        // Check if the agent is a the position of the action
        var dist_to_action = Position.Distance(lumberjack.Position, lumberjack.CurrentAction!.Position);
        if (dist_to_action < 5)
        {
            // Perform action
            lumberjack.CurrentAction.Action();
            lumberjack.CurrentAction = null;

            if (lumberjack.CurrentPlan!.Actions.Count > 0)
            {
                lumberjack.CurrentAction = lumberjack.CurrentPlan?.Actions.FirstOrDefault();
                lumberjack.CurrentPlan!.Actions.Remove(lumberjack.CurrentAction!);
            }
        }
        else
        {
            // Move towards the action position
            var action = lumberjack.CurrentAction;
            var agent_vector = new Vector2((float)lumberjack.Position.X, (float)lumberjack.Position.Y);
            var action_vector = new Vector2((float)action.Position.X, (float)action.Position.Y);
            var dir = action_vector - agent_vector;
            var dir_length = dir.Length();

            lumberjack.Position.X += dir.X * 10.0f / dir_length;
            lumberjack.Position.Y += dir.Y * 10.0f / dir_length;

            //Vector2.Multiply()
            //Vector2.Distance()
        }

        view.Refresh();
    }

    private void StartStopClick(object sender, RoutedEventArgs e)
    {
        timer.IsEnabled = !timer.IsEnabled;
    }

    private void StepClick(object sender, RoutedEventArgs e)
    {
        TimerTick(null, EventArgs.Empty);
    }
}