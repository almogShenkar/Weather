using System;
using WeatherLib;

namespace Sample
{
    /// <summary>
    ///     sample class - demonstrates how to use this libary
    ///     checks results on the log file Weather\Sample\bin\Debug\OpenWeatherMapService.txt
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcom to the Weather App!");
                Console.WriteLine("Please enter location: ");
                var location = new Location(Console.ReadLine());
                OpenWeatherMapService.SetKey("7ee2765dd79071b177532e10e1118cff");
                var service =
                    WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactory.OPEN_WEATHER_MAP);
                var data = service.GetWeatherData(location);
                Console.WriteLine(data);
                Console.WriteLine("press n to exit or any key to try again");
                var c = Console.ReadKey();
                Console.Clear();
                if (c.KeyChar == 'n')
                {
                    Console.WriteLine("Bye Bye!");
                    break;
                }
            }
        }
    }
}