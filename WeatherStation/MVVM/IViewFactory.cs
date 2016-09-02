using System.Windows;

namespace WeatherStation.MVVM
{
    public interface IViewFactory
    {
        Window CreateHistory();
        Window CreateMainWindow();
    }
}