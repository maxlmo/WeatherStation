using System;

namespace WeatherStation.Sensor
{
    public class RandomizedSensor : ISensor
    {
        public static Random Random = new Random();

        public double ReadTemp()
        {
            var randomNumber = Random.NextDouble()*60 - 20;
            return randomNumber;
        }

        public double ReadBar()
        {
            var randomNumber = Random.NextDouble()*70 + 980;
            return randomNumber;
        }
    }
}