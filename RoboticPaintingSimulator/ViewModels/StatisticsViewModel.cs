using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using RoboticPaintingSimulator.Events;
using RoboticPaintingSimulator.Services;

namespace RoboticPaintingSimulator.ViewModels;

public class StatisticsViewModel : INotifyPropertyChanged
{
    private readonly PaintingService _paintingService;
    private int _completed;
    private int _left;
    private int _processedByBlue;
    private int _processedByGreen;
    private int _processedByRed;
    private string _timeElapsed;
    private DispatcherTimer _timer;

    public StatisticsViewModel(PaintingService paintingService, ConfigurationViewModel config)
    {
        _paintingService = paintingService;

        _paintingService.RedRobotCountChanged += count => ProcessedByRed += count;
        _paintingService.BlueRobotCountChanged += count => ProcessedByBlue += count;
        _paintingService.GreenRobotCountChanged += count => ProcessedByGreen += count;

        _paintingService.CompletedElementsCountChanged += count => Completed = count;
        _paintingService.CompletedElementsCountChanged += count => Left = config.ElementCount - count;
        
        EventAggregator.Instance.Subscribe<PaintEvent>(StartTimer);
        EventAggregator.Instance.Subscribe<PaintDoneEvent>(StopTimer);
    }

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

    public event PropertyChangedEventHandler? PropertyChanged;

    private void StartTimer(PaintEvent obj)
    {
        TimeElapsed = "00:00:00";

        _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _timer.Tick += (sender, args) =>
        {
            var time = TimeSpan.Parse(TimeElapsed);
            time = time.Add(TimeSpan.FromSeconds(1));
            TimeElapsed = time.ToString("g");
        };

        _timer.Start();
    }

    private void StopTimer(PaintDoneEvent obj)
    {
        _timer?.Stop();
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}