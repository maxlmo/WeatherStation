using System.Windows;

namespace WeatherStation.Views.History
{
    /// <summary>
    ///     Interaction logic for HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window, IWindow
    {
        public HistoryWindow()
        {
            InitializeComponent();
        }
    }

    public interface IWindow
    {
        void Show();
        void Close();
    }
}