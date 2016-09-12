using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Prism.Events;
using WeatherStation.MVVM;
using WeatherStation.Views.History;

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
            eventAggregator.GetEvent<OpenNewWindow>().Subscribe(OpenNewWindow);
            eventAggregator.GetEvent<CloseWindow>().Subscribe(CloseWindow);
            eventAggregator.GetEvent<WindowClosed>().Subscribe(WindowClosed);
        }

        public void OpenMainWindow()
        {
            var window = _viewFactory.CreateMainWindow(this);
            _windows.Add(window);
            
            window.Show();
        }

        private void WindowClosed(ViewType type)
        {
            var closedWindow = _windows.Find(w => Equals(w.Tag, type));
            _windows.Remove(closedWindow);
        }

        private void CloseWindow(ViewType type)
        {
            var windowToClose = _windows.Find(w => Equals(w.Tag, type));
            _windows.Remove(windowToClose);

            windowToClose.Close();
        }
        
        private void OpenNewWindow(ViewType type)
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

        private Window GetWindowByType(ViewType type)
        {
            switch (type)
            {
                    case ViewType.BarometricPressureHistory:
                        return _viewFactory.CreateBarPressureHistory();
                    case ViewType.TemperatureHistory:
                        return _viewFactory.CreateTemperatureHistory();
                    case ViewType.UnitSettings:
                        return _viewFactory.CreateUnitSettingsWindow();
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