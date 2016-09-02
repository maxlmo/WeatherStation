namespace WeatherStation.Sensor
{
    public interface ISensor
    {
        double ReadTemp();
        double ReadBar();
    }
}