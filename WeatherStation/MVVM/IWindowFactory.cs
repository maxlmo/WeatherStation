using WeatherStation.Views;

namespace WeatherStation.MVVM
{
    public interface IWindowFactory
    {
        IWindow CreateMainWindow();
        IWindow CreateTemperatureHistory();
        IWindow CreateBarPressureHistory();
        IWindow CreateUnitSettingsWindow();
        IWindow CreateDateAndTimeSettingsWindow();
    }
}