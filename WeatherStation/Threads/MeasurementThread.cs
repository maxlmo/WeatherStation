using System;
using System.Threading;
using WeatherStation.Sensor;

namespace WeatherStation.Threads
{
    public class MeasurementThread : IThread
    {
        private readonly ISensor _sensor;
        private bool _threadRunning;
        private Thread _measurementThread;

        public MeasurementThread(ISensor sensor)
        {
            _sensor = sensor;
        }

        public void StartThread()
        {
            _threadRunning = true;
            _measurementThread = new Thread(ThreadLoop);
            _measurementThread.Start();
        }

        public void CloseThread()
        {
            _threadRunning = false;
        }

        private void ThreadLoop()
        {
            while (_threadRunning)
            {
                _sensor.ReadMeasurement();
                Thread.Sleep(1000);
            }
        }
    }
}