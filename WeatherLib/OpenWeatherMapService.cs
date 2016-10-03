using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using log4net;

namespace WeatherLib
{
    /// <summary>
    ///     a weather service to get weather data
    /// </summary>
    public class OpenWeatherMapService : IWeatherDataService
    {
        private static OpenWeatherMapService _instance;
        private static readonly string PreUrl = "http://api.openweathermap.org";
        private static string Key;
        private static readonly string PostUrl = "&mode=xml&appid=";

        private static readonly ILog Log = LogManager.GetLogger
            (MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///     constructor
        /// </summary>
        private OpenWeatherMapService()
        {
            Log.Info("Started");
        }

        /// <summary>
        ///     service implemented as a singleton class
        /// </summary>
        public static OpenWeatherMapService Instance => _instance ?? (_instance = new OpenWeatherMapService());


        public WeatherData GetWeatherData(Location location, TempUnits units)
        {
            try
            {
                var currentWeather = PreUrl + "/data/2.5/weather?";
                var query = "q=";
                string unit;
                Log.Info("User request location: " + location.Name);
                switch (location.GetLocationType())
                {
                    case LocationType.Name:
                        query += location.Name;
                        break;
                    case LocationType.ZipCode:
                        query += location.ZipCode;
                        break;
                    case LocationType.Coord:
                        query += location.Lon + "," + location.Lat;
                        break;
                }
                switch (units)
                {
                    case TempUnits.Kelvin:
                        unit = null;
                        break;
                    case TempUnits.Fahrenheit:
                        unit = "&units=imperial";
                        break;
                    default:
                        unit = "&units=metric";
                        break;
                }
                var request = currentWeather + query + PostUrl + Key + unit;
                var client = new WebClient();
                var document = XDocument.Parse(client.DownloadString(request));
                Log.Info("Data downloaded successfully for location: " + location.Name);
                return ParseXml(document);
            }
            //network exception
            catch (WebException e)
            {
                Log.Error("network exception or bad key");
                throw new WeatherDataServiceException("network exception or bad key");
            }
            //data exception or bad location request
            catch (XmlException e)
            {
                //if the user request a bad location - write to log file, and continue
                if (e.Data.Count == 0)
                {
                    Log.Warn("bad location: " + location.Name);
                    return null;
                }
                Log.Error(e.Message);
                throw new WeatherDataServiceException(e.Message);
            }
        }


        /// <summary>
        ///     key setter to use the service
        /// </summary>
        /// <param name="key"></param>
        public static void SetKey(string key)
        {
            if (key == null) throw new WeatherDataServiceException("missing api key");
            Key = key;
        }

        /// <summary>
        ///     parsing data from xml to WeatherData
        /// </summary>
        /// <param name="document">Data to parse</param>
        /// <returns>the data as a WeatherData</returns>
        public WeatherData ParseXml(XDocument document)
        {
            var data = new WeatherData();
            data.Location.Name = (from city in document.Descendants("city")
                select (string) city.Attribute("name")).First();
            data.Location.ZipCode = (from city in document.Descendants("city")
                select (int) city.Attribute("id")).First();
            data.Location.Lat = (from coord in document.Descendants("coord")
                select (double) coord.Attribute("lat")).First();
            data.Location.Lon = (from coord in document.Descendants("coord")
                select (double) coord.Attribute("lon")).First();
            data.Location.SunRise = (from sun in document.Descendants("sun")
                select (DateTime) sun.Attribute("rise")).First();
            data.Cloud = (from clouds in document.Descendants("clouds")
                select (string) clouds.Attribute("name")).First();
            data.Humidity = (from humidity in document.Descendants("humidity")
                select (int) humidity.Attribute("value")).First();
            data.CurrentTemp = (from temperature in document.Descendants("temperature")
                select (double) temperature.Attribute("value")).First();
            data.MaxTemp = (from temperature in document.Descendants("temperature")
                select (double) temperature.Attribute("max")).First();
            data.MinTemp = (from temperature in document.Descendants("temperature")
                select (double) temperature.Attribute("min")).First();
            data.Pressure = (from pressure in document.Descendants("pressure")
                select (double) pressure.Attribute("value")).First();
            data.WindDesc = (from speed in document.Descendants("speed")
                select (string) speed.Attribute("name")).First();
            data.WindDir = (from direction in document.Descendants("direction")
                select (string) direction.Attribute("name")).First();
            data.LastUpdate = (from lastupdate in document.Descendants("lastupdate")
                select (DateTime) lastupdate.Attribute("value")).First();
            return data;
        }
    }
}