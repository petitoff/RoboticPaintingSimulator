using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RoboticPaintingSimulator.ViewModels;

public class RobotsViewModel : INotifyPropertyChanged
{
    private int _redRobotUsed;
    private int _redRobotAssigned;
    private int _blueRobotUsed;
    private int _blueRobotAssigned;
    private int _greenRobotUsed;
    private int _greenRobotAssigned;
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
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
    
    public RobotsViewModel()
    {
        RedRobotUsed = 0;
        RedRobotAssigned = 0;
        BlueRobotUsed = 0;
        BlueRobotAssigned = 0;
        GreenRobotUsed = 0;
        GreenRobotAssigned = 0;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}