using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

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
                new Agent(new Core.Position(5, 5), Brushes.Blue),
                new Agent(new Core.Position(250, 15), Brushes.Black),
                new Agent(new Core.Position(450, 40), Brushes.Red),
                new Agent(new Core.Position(120, 150), Brushes.Green)
            ];
    }
}