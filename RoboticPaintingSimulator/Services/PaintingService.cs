using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using RoboticPaintingSimulator.Events;
using RoboticPaintingSimulator.Models;
using RoboticPaintingSimulator.ViewModels;

namespace RoboticPaintingSimulator.Services;

public class PaintingService
{
    private readonly object _lock = new();
    private TimeSpan _blueDuration = TimeSpan.FromSeconds(7);
    private SemaphoreSlim _blueSemaphore = new(2);
    private int _currentlyPaintingBlue;

    // Count the number of elements that are currently being painted
    private int _currentlyPaintingElements;
    private int _currentlyPaintingGreen;

    // Counters for currently painting robots
    private int _currentlyPaintingRed;
    private TimeSpan _greenDuration = TimeSpan.FromSeconds(6);
    private SemaphoreSlim _greenSemaphore = new(5);

    // Define different durations for each color
    private TimeSpan _redDuration = TimeSpan.FromSeconds(5);

    // Define semaphores for each color with different maximum concurrent tasks
    private SemaphoreSlim _redSemaphore = new(3);

    public PaintingService(ConfigurationViewModel config)
    {
        config.RedRobotConfig.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(RobotConfig.Count))
                _redSemaphore = new SemaphoreSlim(config.RedRobotConfig.Count);

            if (args.PropertyName == nameof(RobotConfig.ProcessingTime))
                _redDuration = TimeSpan.FromSeconds(config.RedRobotConfig.ProcessingTime);
        };

        config.BlueRobotConfig.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(RobotConfig.Count))
                _blueSemaphore = new SemaphoreSlim(config.BlueRobotConfig.Count);

            if (args.PropertyName == nameof(RobotConfig.ProcessingTime))
                _blueDuration = TimeSpan.FromSeconds(config.BlueRobotConfig.ProcessingTime);
        };

        config.GreenRobotConfig.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(RobotConfig.Count))
                _greenSemaphore = new SemaphoreSlim(config.GreenRobotConfig.Count);

            if (args.PropertyName == nameof(RobotConfig.ProcessingTime))
                _greenDuration = TimeSpan.FromSeconds(config.GreenRobotConfig.ProcessingTime);
        };
    }

    // Count the number of elements that are currently being painted
    public int CompletedElementsCount { get; private set; }

    public int GreenPaintedElements { get; set; }

    public int BluePaintedElements { get; set; }

    public int RedPaintedElements { get; set; }

    public event Action<int> RedToBePaintedChanged;
    public event Action<int> BlueToBePaintedChanged;
    public event Action<int> GreenToBePaintedChanged;

    public event Action<int> CompletedElementsCountChanged;

    public event Action<int> RedRobotCountChanged;
    public event Action<int> BlueRobotCountChanged;
    public event Action<int> GreenRobotCountChanged;

    public async Task PaintAllElementsAsync(ObservableCollection<Element> elements)
    {
        var paintTasks = new List<Task>();

        foreach (var element in elements) paintTasks.Add(PaintElementInAllColorsAsync(element));

        await Task.WhenAll(paintTasks);

        EventAggregator.Instance.Publish(new PaintDoneEvent());
    }

    private async Task PaintElementInAllColorsAsync(Element element)
    {
        var colorTasks = new List<Task>
        {
            PaintElementAsync(element, "Red", _redSemaphore, _redDuration),
            PaintElementAsync(element, "Blue", _blueSemaphore, _blueDuration),
            PaintElementAsync(element, "Green", _greenSemaphore, _greenDuration)
        };

        await Task.WhenAll(colorTasks);

        // Update status to Finished after all colors are painted
        element.Status = "Finished";

        // Sprawdź, czy element został pomalowany we wszystkich kolorach
        if (element.IsRedPainted && element.IsBluePainted && element.IsGreenPainted)
            lock (_lock)
            {
                CompletedElementsCount++;
                CompletedElementsCountChanged?.Invoke(CompletedElementsCount);
            }
    }

    private async Task PaintElementAsync(Element element, string color, SemaphoreSlim semaphore, TimeSpan duration)
    {
        await semaphore.WaitAsync();

        try
        {
            IncrementPaintingCounter(color);

            await Task.Delay(duration); // Simulate painting delay

            switch (color)
            {
                case "Red":
                    element.IsRedPainted = true;
                    RedPaintedElements++;
                    RedToBePaintedChanged?.Invoke(RedPaintedElements);
                    break;
                case "Blue":
                    element.IsBluePainted = true;
                    BluePaintedElements++;
                    BlueToBePaintedChanged?.Invoke(BluePaintedElements);
                    break;
                case "Green":
                    element.IsGreenPainted = true;
                    GreenPaintedElements++;
                    GreenToBePaintedChanged?.Invoke(GreenPaintedElements);
                    break;
            }
        }
        finally
        {
            DecrementPaintingCounter(color);
            semaphore.Release();
        }
    }

    private void IncrementPaintingCounter(string color)
    {
        switch (color)
        {
            case "Red":
                Interlocked.Increment(ref _currentlyPaintingRed);
                RedRobotCountChanged?.Invoke(_currentlyPaintingRed);
                break;
            case "Blue":
                Interlocked.Increment(ref _currentlyPaintingBlue);
                BlueRobotCountChanged?.Invoke(_currentlyPaintingBlue);
                break;
            case "Green":
                Interlocked.Increment(ref _currentlyPaintingGreen);
                GreenRobotCountChanged?.Invoke(_currentlyPaintingGreen);
                break;
        }
    }

    private void DecrementPaintingCounter(string color)
    {
        switch (color)
        {
            case "Red":
                Interlocked.Decrement(ref _currentlyPaintingRed);
                RedRobotCountChanged?.Invoke(_currentlyPaintingRed);
                break;
            case "Blue":
                Interlocked.Decrement(ref _currentlyPaintingBlue);
                BlueRobotCountChanged?.Invoke(_currentlyPaintingBlue);
                break;
            case "Green":
                Interlocked.Decrement(ref _currentlyPaintingGreen);
                GreenRobotCountChanged?.Invoke(_currentlyPaintingGreen);
                break;
        }
    }
}