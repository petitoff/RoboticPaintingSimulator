using RoboticPaintingSimulator.Commands;
using RoboticPaintingSimulator.Events;

namespace RoboticPaintingSimulator.ViewModels;

public class MainViewModel
{
    public RelayCommand PaintCommand { get; }
    
    public MainViewModel(ConfigurationViewModel configurationViewModel, ElementsViewModel elementsViewModel)
    {
        ConfigurationViewModel = configurationViewModel;
        ElementsViewModel = elementsViewModel;
        
        PaintCommand = new RelayCommand(_ => EventAggregator.Instance.Publish(new PaintEvent { Color = "All" }));
    }

    public ConfigurationViewModel ConfigurationViewModel { get; }
    public ElementsViewModel ElementsViewModel { get; }
}