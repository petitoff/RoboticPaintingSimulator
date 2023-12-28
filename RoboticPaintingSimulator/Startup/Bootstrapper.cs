using Autofac;
using RoboticPaintingSimulator.Converters;
using RoboticPaintingSimulator.Events;
using RoboticPaintingSimulator.ViewModels;
using RoboticPaintingSimulator.Views;

namespace RoboticPaintingSimulator.Startup;

public class Bootstrapper
{
    public IContainer Bootstrap()
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<ElementsView>().AsSelf();
        builder.RegisterType<ElementsViewModel>().AsSelf();

        builder.RegisterType<ConfigurationView>().AsSelf();
        builder.RegisterType<ConfigurationViewModel>().SingleInstance();

        builder.RegisterType<MainWindow>().AsSelf();
        builder.RegisterType<MainViewModel>().AsSelf();
        
        builder.RegisterType<RobotsViewModel>().AsSelf();
        builder.RegisterType<RobotsView>().AsSelf();

        return builder.Build();
    }
}