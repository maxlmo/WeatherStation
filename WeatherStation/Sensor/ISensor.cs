namespace WeatherStation.Sensor
{
    public interface ISensor
    {
        void ReadTemp();
        void ReadBar();
    }
}