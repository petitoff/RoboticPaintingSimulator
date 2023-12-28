using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using RoboticPaintingSimulator.Models;

namespace RoboticPaintingSimulator.Services;

public class PaintingService
{
    // Define semaphores for each color with different maximum concurrent tasks
    private readonly SemaphoreSlim _redSemaphore = new(3);
    private readonly SemaphoreSlim _blueSemaphore = new(2);
    private readonly SemaphoreSlim _greenSemaphore = new(5);

    // Define different durations for each color
    private readonly TimeSpan _redDuration = TimeSpan.FromSeconds(5);
    private readonly TimeSpan _blueDuration = TimeSpan.FromSeconds(7);
    private readonly TimeSpan _greenDuration = TimeSpan.FromSeconds(6);

    public async Task PaintAllElementsAsync(ObservableCollection<Element> elements)
    {
        var paintTasks = new List<Task>();

        foreach (var element in elements)
        {
            paintTasks.Add(PaintElementInAllColorsAsync(element));
        }

        await Task.WhenAll(paintTasks);
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
            // Update status to Painting
            element.Status = "Painting";

            // Simulate a delay for the painting operation
            await Task.Delay(duration);

            switch (color)
            {
                case "Red":
                    element.IsRedPainted = true;
                    break;
                case "Blue":
                    element.IsBluePainted = true;
                    break;
                case "Green":
                    element.IsGreenPainted = true;
                    break;
            }
        }
        finally
        {
            semaphore.Release();
        }
    }
}