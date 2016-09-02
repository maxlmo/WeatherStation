using NUnit.Framework;
using WeatherStation.Sensor;

namespace WeatherStationTest.Sensor
{
    [TestFixture]
    public class RandomizedSensorTest
    {
        [Test, Repeat(100)]
        public void ReadBar_ReturnDouble_WithInRange()
        {
            var classUnderTest = new RandomizedSensor();

            var result = classUnderTest.ReadBar();

            const int minPressure = 980;
            const int maxPressure = 1050;
            Assert.That(result, Is.InRange(minPressure, maxPressure));
        }

        [Test, Repeat(100)]
        public void ReadTemp_ReturnsDouble_WithInRange()
        {
            var classUnderTest = new RandomizedSensor();

            var result = classUnderTest.ReadTemp();

            const int minTemperature = -20;
            const int maxTemperature = 40;
            Assert.That(result, Is.InRange(minTemperature, maxTemperature));
        }
    }
}