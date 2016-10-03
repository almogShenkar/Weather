namespace WeatherLib
{
    /// <summary>
    ///     services thats support this libary
    /// </summary>
    public enum ServiceName
    {
        OpenWeatherMap
    }

    /// <summary>
    ///     WeatherDataServiceFactory creates service by enum ServiceName
    /// </summary>
    public class WeatherDataServiceFactory
    {
        public static ServiceName OPEN_WEATHER_MAP = ServiceName.OpenWeatherMap;

        /// <summary>
        ///     get weather service according to ServiceName
        /// </summary>
        /// <param name="name">the service name</param>
        /// <returns>service implements IweatherDataService</returns>
        public static IWeatherDataService GetWeatherDataService(ServiceName name)
        {
            if (name == OPEN_WEATHER_MAP) return OpenWeatherMapService.Instance;
            throw new WeatherDataServiceException("service name exception");
        }
    }
}