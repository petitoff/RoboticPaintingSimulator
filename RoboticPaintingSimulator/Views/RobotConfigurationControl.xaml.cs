using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RoboticPaintingSimulator.Views;

public partial class RobotConfigurationControl : UserControl
{
    public RobotConfigurationControl()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty RobotColorProperty = DependencyProperty.Register(
        "RobotColor",
        typeof(Brush),
        typeof(RobotConfigurationControl),
        new PropertyMetadata(Brushes.Red)); // Default value set to Red

    public Brush RobotColor
    {
        get { return (Brush)GetValue(RobotColorProperty); }
        set { SetValue(RobotColorProperty, value); }
    }

}