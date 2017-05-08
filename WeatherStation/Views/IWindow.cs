using System;
using System.ComponentModel;

namespace WeatherStation.Views
{
    public interface IWindow
    {
        void Show();
        void Close();
        object Tag { get; set; }
        bool Activate();
        event CancelEventHandler Closing;
    }
}