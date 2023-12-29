using System;
using System.Collections.Concurrent;
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
    // Define semaphores for each color with different maximum concurrent tasks
    private SemaphoreSlim _redSemaphore = new(3);
    private SemaphoreSlim _blueSemaphore = new(2);
    private SemaphoreSlim _greenSemaphore = new(5);

    // Define different durations for each color
    private TimeSpan _redDuration = TimeSpan.FromSeconds(5);
    private TimeSpan _blueDuration = TimeSpan.FromSeconds(7);
    private TimeSpan _greenDuration = TimeSpan.FromSeconds(6);

    // Counters for currently painting robots
    private int _currentlyPaintingRed;
    private int _currentlyPaintingBlue;
    private int _currentlyPaintingGreen;

    public event Action<int> RedRobotCountChanged;
    public event Action<int> BlueRobotCountChanged;
    public event Action<int> GreenRobotCountChanged;
    
    // Count the number of elements that are currently being painted
    private int _currentlyPaintingElements;

    //Count the number of painted elements for each color
    public int RedPaintedElements;
    public int BluePaintedElements;
    public int GreenPaintedElements;

    private readonly ConfigurationViewModel _config;

    public PaintingService(ConfigurationViewModel config)
    {
        _config = config;

        _config.RedRobotConfig.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(RobotConfig.Count))
            {
                _redSemaphore = new SemaphoreSlim(_config.RedRobotConfig.Count);
            }

            if (args.PropertyName == nameof(RobotConfig.ProcessingTime))
            {
                _redDuration = TimeSpan.FromSeconds(_config.RedRobotConfig.ProcessingTime);
            }
        };

        _config.BlueRobotConfig.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(RobotConfig.Count))
            {
                _blueSemaphore = new SemaphoreSlim(_config.BlueRobotConfig.Count);
            }

            if (args.PropertyName == nameof(RobotConfig.ProcessingTime))
            {
                _blueDuration = TimeSpan.FromSeconds(_config.BlueRobotConfig.ProcessingTime);
            }
        };

        _config.GreenRobotConfig.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(RobotConfig.Count))
            {
                _greenSemaphore = new SemaphoreSlim(_config.GreenRobotConfig.Count);
            }

            if (args.PropertyName == nameof(RobotConfig.ProcessingTime))
            {
                _greenDuration = TimeSpan.FromSeconds(_config.GreenRobotConfig.ProcessingTime);
            }
        };
    }

    public async Task PaintAllElementsAsync(ObservableCollection<Element> elements)
    {
        var paintTasks = new List<Task>();

        foreach (var element in elements)
        {
            paintTasks.Add(PaintElementInAllColorsAsync(element));
        }

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
    }

    private async Task PaintElementAsync(Element element, string color, SemaphoreSlim semaphore, TimeSpan duration)
    {
        await semaphore.WaitAsync();

        try
        {
            IncrementPaintingCounter(color);

            // Update status to Painting
            element.Status = "Painting";

            // Simulate a delay for the painting operation
            await Task.Delay(duration);

            switch (color)
            {
                case "Red":
                    element.IsRedPainted = true;
                    RedPaintedElements++;
                    break;
                case "Blue":
                    element.IsBluePainted = true;
                    BluePaintedElements++;
                    break;
                case "Green":
                    element.IsGreenPainted = true;
                    GreenPaintedElements++;
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