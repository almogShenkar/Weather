using System;

namespace WeatherLib
{
    /// <summary>
    ///     three unit representations
    /// </summary>
    public enum TempUnits
    {
        Celsius,
        Fahrenheit,
        Kelvin
    }

    /// <summary>
    ///     all the relevant properties for weather data
    /// </summary>
    public class WeatherData
    {
        public WeatherData()
        {
            Location = new Location();
        }

        public Location Location { set; get; }
        public string WindDesc { get; set; }
        public string WindDir { get; set; }
        public string Cloud { get; set; }
        public double CurrentTemp { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public int Humidity { get; set; }
        public double Pressure { get; set; }
        public DateTime LastUpdate { get; set; }


        /// <summary>
        ///     aux method for printing
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Location: " + Location.Name + "\n" +
                   "Temp now: " + CurrentTemp + "\n" +
                   "Temp max: " + MaxTemp + "\n" +
                   "Temp min: " + MinTemp + "\n" +
                   "Humidity: " + Humidity + "\n" +
                   "Pressure: " + Pressure + "\n" +
                   "Clouds: " + Cloud + "\n" +
                   "Wind : " + WindDesc + " " + WindDir + "\n" +
                   "Last update: " + LastUpdate + "\n";
        }
    }
}