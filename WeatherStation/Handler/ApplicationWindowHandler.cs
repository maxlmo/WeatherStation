using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Events;
using WeatherStation.MVVM;
using WeatherStation.Views;

namespace WeatherStation.Handler
{
    public class ApplicationWindowHandler
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowFactory _windowFactory;
        private readonly List<IWindow> _windows = new List<IWindow>();

        public ApplicationWindowHandler(IEventAggregator eventAggregator, IWindowFactory windowFactory)
        {
            _eventAggregator = eventAggregator;
            _windowFactory = windowFactory;
        }

        public void WindowClosed(WindowType type)
        {
            var closedWindow = _windows.Find(w => Equals(w.Tag, type));
            _windows.Remove(closedWindow);
        }

        public void CloseWindow(WindowType type)
        {
            var windowToClose = _windows.Find(w => Equals(w.Tag, type));
            _windows.Remove(windowToClose);

            windowToClose.Close();
        }

        public void OpenNewWindow(WindowType type)
        {
            if (_windows.Any(w => Equals(w.Tag, type)))
            {
                _windows.Find(w => Equals(w.Tag, type)).Activate();
            }
            else
            {
                var window = GetWindowByType(type);
                _windows.Add(window);
                window.Closing += delegate { _eventAggregator.GetEvent<WindowClosed>().Publish(type); };
                window.Show();
            }
        }

        private IWindow GetWindowByType(WindowType type)
        {
            switch (type)
            {
                case WindowType.BarometricPressureHistory:
                    return _windowFactory.CreateBarPressureHistory();
                case WindowType.TemperatureHistory:
                    return _windowFactory.CreateTemperatureHistory();
                case WindowType.UnitSettings:
                    return _windowFactory.CreateUnitSettingsWindow();
                case WindowType.MainWindow:
                    return _windowFactory.CreateMainWindow();
                case WindowType.DateAndTimeSettings:
                    return _windowFactory.CreateDateAndTimeSettingsWindow();
            }
            throw new NotSupportedException();
        }
    }

    public class WindowClosed : PubSubEvent<WindowType>
    {
    }

    public class CloseWindow : PubSubEvent<WindowType>
    {
    }

    public class OpenNewWindow : PubSubEvent<WindowType>
    {
    }
}