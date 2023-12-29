using RoboticPaintingSimulator.Commands;
using RoboticPaintingSimulator.Events;

namespace RoboticPaintingSimulator.ViewModels;

public class MainViewModel
{
    public MainViewModel(ConfigurationViewModel configurationViewModel, ElementsViewModel elementsViewModel,
        RobotsViewModel robotsViewModel, StatisticsViewModel statisticsViewModel)
    {
        ConfigurationViewModel = configurationViewModel;
        ElementsViewModel = elementsViewModel;
        RobotsViewModel = robotsViewModel;
        StatisticsViewModel = statisticsViewModel;

        PaintCommand = new RelayCommand(_ => EventAggregator.Instance.Publish(new PaintEvent { Color = "All" }));
    }

    public MainViewModel()
    {
    }

    public RelayCommand PaintCommand { get; }

    public ConfigurationViewModel ConfigurationViewModel { get; }
    public ElementsViewModel ElementsViewModel { get; }
    public RobotsViewModel RobotsViewModel { get; }
    public StatisticsViewModel StatisticsViewModel { get; }
}