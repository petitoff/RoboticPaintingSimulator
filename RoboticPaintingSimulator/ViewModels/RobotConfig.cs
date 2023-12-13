using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using RoboticPaintingSimulator.Commands;

namespace RoboticPaintingSimulator.ViewModels;

public class RobotConfig : INotifyPropertyChanged
{
    private int _count;
    private int _processingTime;

    public int Count
    {
        get => _count;
        set
        {
            if (_count != value)
            {
                _count = value;
                OnPropertyChanged();
            }
        }
    }

    public int ProcessingTime
    {
        get => _processingTime;
        set
        {
            if (_processingTime != value)
            {
                _processingTime = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand IncrementCountCommand { get; }
    public ICommand DecrementCountCommand { get; }
    public ICommand IncrementProcessingTimeCommand { get; }
    public ICommand DecrementProcessingTimeCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public RobotConfig()
    {
        IncrementCountCommand = new RelayCommand(Increment);
        DecrementCountCommand = new RelayCommand(Decrement, CamDecrement);
        IncrementProcessingTimeCommand = new RelayCommand(IncrementProcessingTime);
        DecrementProcessingTimeCommand = new RelayCommand(DecrementProcessingTime);
    }

    private void Increment(object? obj) => Count++;
    private void Decrement(object? obj) => Count--;

    private bool CamDecrement(object? obj)
    {
        return Count > 0;
    }

    private void IncrementProcessingTime(object? obj) => ProcessingTime++;
    private void DecrementProcessingTime(object? obj) => ProcessingTime--;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}