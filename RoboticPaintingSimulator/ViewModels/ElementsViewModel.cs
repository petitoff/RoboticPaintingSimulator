using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RoboticPaintingSimulator.Events;
using RoboticPaintingSimulator.Models;
using RoboticPaintingSimulator.Services;

namespace RoboticPaintingSimulator.ViewModels;

public class ElementsViewModel : INotifyPropertyChanged
{
    private readonly PaintingService _paintingService;
    private ObservableCollection<Element> _elements;

    public ElementsViewModel()
    {
        _paintingService = new PaintingService();
        Elements = new ObservableCollection<Element>();
        EventAggregator.Instance.Subscribe<PaintEvent>(InitializeElements);
    }

    public ObservableCollection<Element> Elements
    {
        get => _elements;
        set
        {
            _elements = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void InitializeElements(PaintEvent paintEvent)
    {
        for (var i = 0; i < 3; i++) Elements.Add(new Element { Status = "Idle" });
        StartPaintingProcess();
    }

    private async void StartPaintingProcess()
    {
        try
        {
            await _paintingService.PaintAllElementsAsync(Elements);
        }
        catch (Exception ex)
        {
            // Handle exceptions
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}