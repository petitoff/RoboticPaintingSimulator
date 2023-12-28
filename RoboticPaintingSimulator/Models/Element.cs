using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RoboticPaintingSimulator.Models;

public class Element : INotifyPropertyChanged
{
    private int _id;
    private bool _isBluePainted;
    private bool _isGreenPainted;
    private bool _isRedPainted;
    private string _status;

    public int Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged();
        }
    }

    public bool IsRedPainted
    {
        get => _isRedPainted;
        set
        {
            _isRedPainted = value;
            OnPropertyChanged();
        }
    }

    public bool IsBluePainted
    {
        get => _isBluePainted;
        set
        {
            _isBluePainted = value;
            OnPropertyChanged();
        }
    }

    public bool IsGreenPainted
    {
        get => _isGreenPainted;
        set
        {
            _isGreenPainted = value;
            OnPropertyChanged();
        }
    }

    public string Status
    {
        get => _status;
        set
        {
            _status = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}