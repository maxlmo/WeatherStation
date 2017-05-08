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
        private readonly IViewFactory _viewFactory;
        private readonly List<IWindow> _windows = new List<IWindow>();

        public ApplicationWindowHandler(IEventAggregator eventAggregator, IViewFactory viewFactory)
        {
            _eventAggregator = eventAggregator;
            _viewFactory = viewFactory;
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
                    return _viewFactory.CreateBarPressureHistory();
                case WindowType.TemperatureHistory:
                    return _viewFactory.CreateTemperatureHistory();
                case WindowType.UnitSettings:
                    return _viewFactory.CreateUnitSettingsWindow();
                case WindowType.MainWindow:
                    return _viewFactory.CreateMainWindow();
                case WindowType.DateAndTimeSettings:
                    return _viewFactory.CreateDateAndTimeSettingsWindow();
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