using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RoboticPaintingSimulator.ViewModels;

public class StatisticsViewModel : INotifyPropertyChanged
{
    private string _timeElapsed;
    private int _completed;
    private int _left;
    private int _processedByRed;
    private int _processedByBlue;
    private int _processedByGreen;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string TimeElapsed
    {
        get => _timeElapsed;
        set
        {
            _timeElapsed = value;
            OnPropertyChanged();
        }
    }

    public int Completed
    {
        get => _completed;
        set
        {
            _completed = value;
            OnPropertyChanged();
        }
    }

    public int Left
    {
        get => _left;
        set
        {
            _left = value;
            OnPropertyChanged();
        }
    }

    public int ProcessedByRed
    {
        get => _processedByRed;
        set
        {
            _processedByRed = value;
            OnPropertyChanged();
        }
    }

    public int ProcessedByBlue
    {
        get => _processedByBlue;
        set
        {
            _processedByBlue = value;
            OnPropertyChanged();
        }
    }

    public int ProcessedByGreen
    {
        get => _processedByGreen;
        set
        {
            _processedByGreen = value;
            OnPropertyChanged();
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}