namespace WeatherLib
{
    /// <summary>
    ///     interface to use any weather service
    /// </summary>
    public interface IWeatherDataService
    {
        /// <summary>
        ///     Get current weather by location
        /// </summary>
        /// <param name="location">the requested location</param>
        /// <param name="units">requested unit</param>
        /// <returns></returns>
        WeatherData GetWeatherData(Location location, TempUnits units = TempUnits.Celsius);
    }
}