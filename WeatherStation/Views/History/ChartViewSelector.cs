using System.Windows;
using System.Windows.Controls;
using WeatherStation.ViewModels;

namespace WeatherStation.Views.History
{
    public class ChartViewSelector : DataTemplateSelector
    {
        public DataTemplate BarometricPressureChartControl { get; set; }
        public DataTemplate TemperatureChartControl { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return null;
            }
            if (item.GetType() == typeof(TemperatureHistoryWindowViewModel))
            {
                return TemperatureChartControl;
            }
            return BarometricPressureChartControl;
        }
    }
}