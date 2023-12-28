using System.Windows;
using RoboticPaintingSimulator.ViewModels;

namespace RoboticPaintingSimulator.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public MainWindow()
        {
            throw new System.NotImplementedException();
        }
    }
}