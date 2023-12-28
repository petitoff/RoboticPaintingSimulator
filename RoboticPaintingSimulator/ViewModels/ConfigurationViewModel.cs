using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using RoboticPaintingSimulator.Commands;

namespace RoboticPaintingSimulator.ViewModels;

public class ConfigurationViewModel : INotifyPropertyChanged
{
    private int _elementCount;

    public ConfigurationViewModel()
    {
        IncrementElementCountCommand = new RelayCommand(_ => IncrementElementCount());
        DecrementElementCountCommand = new RelayCommand(_ => DecrementElementCount());
    }

    public RobotConfig RedRobotConfig { get; } = new();
    public RobotConfig BlueRobotConfig { get; } = new();
    public RobotConfig GreenRobotConfig { get; } = new();

    public ICommand IncrementElementCountCommand { get; private set; }
    public ICommand DecrementElementCountCommand { get; private set; }

    public int ElementCount
    {
        get => _elementCount;
        set
        {
            if (_elementCount != value)
            {
                _elementCount = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void IncrementElementCount()
    {
        ElementCount++;
    }

    private void DecrementElementCount()
    {
        if (ElementCount > 0)
            ElementCount--;
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}