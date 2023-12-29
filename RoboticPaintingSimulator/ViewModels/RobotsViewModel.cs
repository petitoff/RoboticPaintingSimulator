using System.ComponentModel;
using System.Runtime.CompilerServices;
using RoboticPaintingSimulator.Services;

namespace RoboticPaintingSimulator.ViewModels;

public class RobotsViewModel : INotifyPropertyChanged
{
    private readonly PaintingService _paintingService;

    private int _blueDuration;
    private int _blueRobotAssigned;
    private int _blueRobotUsed;

    private int _greenDuration;
    private int _greenRobotAssigned;
    private int _greenRobotUsed;

    private int _redDuration;
    private int _redRobotAssigned;
    private int _redRobotUsed;

    public RobotsViewModel(PaintingService paintingService, ConfigurationViewModel config)
    {
        _paintingService = paintingService;

        config.RedRobotConfig.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(RobotConfig.Count))
                RedRobotAssigned = config.RedRobotConfig.Count;

            if (args.PropertyName == nameof(RobotConfig.ProcessingTime))
                RedDuration = config.RedRobotConfig.ProcessingTime;
        };

        config.BlueRobotConfig.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(RobotConfig.Count))
                BlueRobotAssigned = config.BlueRobotConfig.Count;

            if (args.PropertyName == nameof(RobotConfig.ProcessingTime))
                BlueDuration = config.BlueRobotConfig.ProcessingTime;
        };

        config.GreenRobotConfig.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(RobotConfig.Count))
                GreenRobotAssigned = config.GreenRobotConfig.Count;

            if (args.PropertyName == nameof(RobotConfig.ProcessingTime))
                GreenDuration = config.GreenRobotConfig.ProcessingTime;
        };

        UpdateRobotConfig();
    }

    public int GreenDuration
    {
        get => _greenDuration;
        set
        {
            if (_greenDuration != value)
            {
                _greenDuration = value;
                OnPropertyChanged();
            }
        }
    }

    public int BlueDuration
    {
        get => _blueDuration;
        set
        {
            if (_blueDuration != value)
            {
                _blueDuration = value;
                OnPropertyChanged();
            }
        }
    }

    public int RedDuration
    {
        get => _redDuration;
        set
        {
            if (_redDuration != value)
            {
                _redDuration = value;
                OnPropertyChanged();
            }
        }
    }

    public int RedRobotUsed
    {
        get => _redRobotUsed;
        set
        {
            if (_redRobotUsed != value)
            {
                _redRobotUsed = value;
                OnPropertyChanged();
            }
        }
    }

    public int RedRobotAssigned
    {
        get => _redRobotAssigned;
        set
        {
            if (_redRobotAssigned != value)
            {
                _redRobotAssigned = value;
                OnPropertyChanged();
            }
        }
    }

    public int BlueRobotUsed
    {
        get => _blueRobotUsed;
        set
        {
            if (_blueRobotUsed != value)
            {
                _blueRobotUsed = value;
                OnPropertyChanged();
            }
        }
    }

    public int BlueRobotAssigned
    {
        get => _blueRobotAssigned;
        set
        {
            if (_blueRobotAssigned != value)
            {
                _blueRobotAssigned = value;
                OnPropertyChanged();
            }
        }
    }

    public int GreenRobotUsed
    {
        get => _greenRobotUsed;
        set
        {
            if (_greenRobotUsed != value)
            {
                _greenRobotUsed = value;
                OnPropertyChanged();
            }
        }
    }

    public int GreenRobotAssigned
    {
        get => _greenRobotAssigned;
        set
        {
            if (_greenRobotAssigned != value)
            {
                _greenRobotAssigned = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void UpdateRobotConfig()
    {
        _paintingService.RedRobotCountChanged += count => RedRobotUsed = count;
        _paintingService.BlueRobotCountChanged += count => BlueRobotUsed = count;
        _paintingService.GreenRobotCountChanged += count => GreenRobotUsed = count;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}