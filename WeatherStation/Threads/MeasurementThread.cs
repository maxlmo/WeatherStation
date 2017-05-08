using System.Threading;
using WeatherStation.Sensor;

namespace WeatherStation.Threads
{
    public abstract class MeasurementThread : IThread
    {
        private readonly ISensor _sensor;
        private bool _threadRunning;
        private Thread _measurementThread;

        protected MeasurementThread(ISensor sensor)
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

        protected abstract int GetMeasurementInterval();
        protected abstract void SendWaitUpdate(int timeToWait);

        private void ThreadLoop()
        {
            while (_threadRunning)
            {
                var waitCounter = 0;
                _sensor.ReadMeasurement();
                while (waitCounter != GetMeasurementInterval() && _threadRunning)
                {
                    Thread.Sleep(1000);
                    waitCounter++;
                    SendWaitUpdate(GetMeasurementInterval() - waitCounter);
                }
            }
        }

    }
}