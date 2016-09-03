using System.Windows;
using Prism.Events;
using WeatherStation.MVVM;
using WeatherStation.Sensor;

namespace WeatherStation
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var eventAggregator = new EventAggregator();
            var sensor = new RandomizedSensor(eventAggregator);
            var viewFactory = new MvvmViewFactory(eventAggregator, sensor);
            var mainWindow = viewFactory.CreateMainWindow();

            mainWindow.Show();
        }
    }
}