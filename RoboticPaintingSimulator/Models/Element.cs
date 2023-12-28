using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RoboticPaintingSimulator.Models;

public class Element : INotifyPropertyChanged
{
    private bool _isRedPainted;
    private bool _isBluePainted;
    private bool _isGreenPainted;
    private string _status;

    public bool IsRedPainted
    {
        get => _isRedPainted;
        set { _isRedPainted = value; OnPropertyChanged(); }
    }

    public bool IsBluePainted
    {
        get => _isBluePainted;
        set { _isBluePainted = value; OnPropertyChanged(); }
    }

    public bool IsGreenPainted
    {
        get => _isGreenPainted;
        set { _isGreenPainted = value; OnPropertyChanged(); }
    }

    public string Status
    {
        get => _status;
        set { _status = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
