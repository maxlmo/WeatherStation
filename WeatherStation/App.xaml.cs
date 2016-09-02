using System.Windows;
using WeatherStation.MVVM;

namespace WeatherStation
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var viewFactory = new MvvmViewFactory();
            var mainWindow = viewFactory.CreateMainWindow();

            mainWindow.Show();
        }
    }
}