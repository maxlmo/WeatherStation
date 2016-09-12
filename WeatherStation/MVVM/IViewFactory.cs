using WeatherStation.Handler;
using WeatherStation.Views;
using WeatherStation.Views.History;

namespace WeatherStation.MVVM
{
    public interface IViewFactory
    {
        IWindow CreateMainWindow();
        IWindow CreateTemperatureHistory();
        IWindow CreateBarPressureHistory();
        IWindow CreateUnitSettingsWindow();
    }
}