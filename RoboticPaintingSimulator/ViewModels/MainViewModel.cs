using RoboticPaintingSimulator.Commands;
using RoboticPaintingSimulator.Events;

namespace RoboticPaintingSimulator.ViewModels;

public class MainViewModel
{
    public RelayCommand PaintCommand { get; }
    
    public MainViewModel(ConfigurationViewModel configurationViewModel, ElementsViewModel elementsViewModel, RobotsViewModel robotsViewModel)
    {
        ConfigurationViewModel = configurationViewModel;
        ElementsViewModel = elementsViewModel;
        RobotsViewModel = robotsViewModel;

        PaintCommand = new RelayCommand(_ => EventAggregator.Instance.Publish(new PaintEvent { Color = "All" }));
    }

    public MainViewModel()
    {
        
    }

    public ConfigurationViewModel ConfigurationViewModel { get; }
    public ElementsViewModel ElementsViewModel { get; }
    public RobotsViewModel RobotsViewModel { get; }
}