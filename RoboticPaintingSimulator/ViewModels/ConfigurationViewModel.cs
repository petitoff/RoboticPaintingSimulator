using System.ComponentModel;
using System.Windows.Input;
using RoboticPaintingSimulator.Commands;

namespace RoboticPaintingSimulator.ViewModels;

public class ConfigurationViewModel : INotifyPropertyChanged
{
    public RobotConfig RedRobotConfig { get; } = new RobotConfig();
    public RobotConfig BlueRobotConfig { get; } = new RobotConfig();
    public RobotConfig GreenRobotConfig { get; } = new RobotConfig();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}