using System.Collections.ObjectModel;
using System.Windows;

namespace VirtualVillage;

public partial class MainWindow : Window
{
    public ObservableCollection<Agent> Agents
    {
        get { return (ObservableCollection<Agent>)GetValue(AgentsProperty); }
        set { SetValue(AgentsProperty, value); }
    }
    public static readonly DependencyProperty AgentsProperty =
        DependencyProperty.Register(nameof(Agents), typeof(ObservableCollection<Agent>), typeof(MainWindow), new PropertyMetadata(null));

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        Agents = 
            [
                new Agent(new Core.Position(5, 5)),
                new Agent(new Core.Position(25, 15)),
                new Agent(new Core.Position(55, 40)),
                new Agent(new Core.Position(12, 50))
            ];
    }
}