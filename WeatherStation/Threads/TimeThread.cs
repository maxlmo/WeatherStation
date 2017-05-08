using System;
using System.Threading;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Model;

namespace WeatherStation.Threads
{
    public class TimeThread : IThread
    {
        private readonly IEventAggregator _eventAggregator;
        private Thread _timeThread;
        private bool _threadRunning;

        public TimeThread(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void StartThread()
        {
            _timeThread = new Thread(ThreadLoop);
            _threadRunning = true;
            _timeThread.Start();

        }

        private void ThreadLoop()
        {
            while (_threadRunning)
            {
                var currentDateTime = DateTime.Now;
                _eventAggregator.GetEvent<DateTimeChanged>().Publish(currentDateTime);
                Thread.Sleep(1000);
            }
        }

        public void CloseThread()
        {
            _threadRunning = false;
        }
    }
}