using Autofac;
using RoboticPaintingSimulator.Converters;
using RoboticPaintingSimulator.Events;
using RoboticPaintingSimulator.Services;
using RoboticPaintingSimulator.ViewModels;
using RoboticPaintingSimulator.Views;

namespace RoboticPaintingSimulator.Startup;

public class Bootstrapper
{
    public IContainer Bootstrap()
    {
        var builder = new ContainerBuilder();
        
        builder.RegisterType<PaintingService>().AsSelf().SingleInstance();

        builder.RegisterType<ElementsView>().AsSelf();
        builder.RegisterType<ElementsViewModel>().AsSelf();

        builder.RegisterType<ConfigurationView>().AsSelf();
        builder.RegisterType<ConfigurationViewModel>().AsSelf().SingleInstance();
        
        builder.RegisterType<RobotsViewModel>().AsSelf();
        builder.RegisterType<RobotsView>().AsSelf();
        
        builder.RegisterType<StatisticsViewModel>().AsSelf();
        builder.RegisterType<StatisticsView>().AsSelf();

        builder.RegisterType<MainWindow>().AsSelf();
        builder.RegisterType<MainViewModel>().AsSelf();
        
        return builder.Build();
    }
}