using System;
using Moq;
using NUnit.Framework;
using Prism.Events;
using WeatherStation.Messages;
using WeatherStation.Sensor;

namespace WeatherStation.Test.Sensor
{
    [TestFixture]
    public class RandomizedSensorTest
    {
        [Test, Repeat(100)]
        public void ReadBar_ReturnDouble_WithInRange()
        {
        }

        [Test, Repeat(100)]
        public void ReadTemp_ReturnsDouble_WithInRange()
        {
            //var classUnderTest = new RandomizedSensor();

            //var result = classUnderTest.ReadTemp();

            //const int minTemperature = -20;
            //const int maxTemperature = 40;
            //Assert.That(result, Is.InRange(minTemperature, maxTemperature));
        }
    }
}