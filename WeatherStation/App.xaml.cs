using System.Windows;
using Prism.Events;
using WeatherStation.MVVM;
using WeatherStation.Sensor;
using WeatherStation.Threads;

namespace WeatherStation
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Bootstrap _bootstrap;

        protected override void OnStartup(StartupEventArgs e)
        {
            _bootstrap = new Bootstrap();
            _bootstrap.StartApplication();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _bootstrap.CloseThreads();
        }
    }
}