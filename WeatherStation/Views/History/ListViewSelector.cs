using System.Windows;
using System.Windows.Controls;
using WeatherStation.ViewModels;

namespace WeatherStation.Views.History
{
    public class ListViewSelector: DataTemplateSelector
    {
        public DataTemplate BarometricPressureListControl { get; set; }
        public DataTemplate TemperatureListControl { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return null;
            }
            if (item.GetType() == typeof(TemperatureHistoryWindowViewModel))
            {
                return TemperatureListControl;
            }
            return BarometricPressureListControl;
        }
    }
}
