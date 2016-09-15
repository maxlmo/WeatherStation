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

        public void WindowClosed(ViewType type)
        {
            var closedWindow = _windows.Find(w => Equals(w.Tag, type));
            _windows.Remove(closedWindow);
        }

        public void CloseWindow(ViewType type)
        {
            var windowToClose = _windows.Find(w => Equals(w.Tag, type));
            _windows.Remove(windowToClose);

            windowToClose.Close();
        }

        public void OpenNewWindow(ViewType type)
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

        private IWindow GetWindowByType(ViewType type)
        {
            switch (type)
            {
                case ViewType.BarometricPressureHistory:
                    return _viewFactory.CreateBarPressureHistory();
                case ViewType.TemperatureHistory:
                    return _viewFactory.CreateTemperatureHistory();
                case ViewType.UnitSettings:
                    return _viewFactory.CreateUnitSettingsWindow();
                case ViewType.MainWindow:
                    return _viewFactory.CreateMainWindow();
                case ViewType.DateAndTimeSettings:
                    return _viewFactory.CreateDateAndTimeSettingsWindow();
            }
            throw new NotSupportedException();
        }
    }

    public class WindowClosed : PubSubEvent<ViewType>
    {
    }

    public class CloseWindow : PubSubEvent<ViewType>
    {
    }

    public class OpenNewWindow : PubSubEvent<ViewType>
    {
    }
}