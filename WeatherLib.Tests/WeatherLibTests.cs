using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeatherLib.Tests
{
    [TestClass]
    public class WeatherLibTests
    {
        /// <summary>
        ///     A test for a complete session - data+ network
        /// </summary>
        [TestMethod]
        public void GetWeatherDataTest()
        {
            var location = new Location("Tel aviv");
            OpenWeatherMapService.SetKey("7ee2765dd79071b177532e10e1118cff");
            var service = WeatherDataServiceFactory.GetWeatherDataService(ServiceName.OpenWeatherMap);
            var data = service.GetWeatherData(location);
            Assert.IsNotNull(data);
        }

        /// <summary>
        ///     A test for check if parsing process is correctly
        /// </summary>
        [TestMethod]
        public void ParseXmlDataTest()
        {
            var doc = XDocument.Load("../../Mock/weatherMock.xml");
            var service = OpenWeatherMapService.Instance;
            var data = service.ParseXml(doc);
            Assert.AreEqual(data.Location.Name, (string) doc.Descendants("city").Attributes("name").First());
            Assert.AreEqual(data.Cloud, (string) doc.Descendants("clouds").Attributes("name").First());
            Assert.AreEqual(data.Humidity, (int) doc.Descendants("humidity").Attributes("value").First());
            Assert.AreEqual(data.LastUpdate, (DateTime) doc.Descendants("lastupdate").Attributes("value").First());
            Assert.AreEqual(data.Pressure, (double) doc.Descendants("pressure").Attributes("value").First());
            Assert.AreEqual(data.WindDesc, (string) doc.Descendants("speed").Attributes("name").First());
            Assert.AreEqual(data.WindDir, (string) doc.Descendants("direction").Attributes("name").First());
            Assert.AreEqual(data.CurrentTemp, (double) doc.Descendants("temperature").Attributes("value").First());
            Assert.AreEqual(data.MaxTemp, (double) doc.Descendants("temperature").Attributes("max").First());
            Assert.AreEqual(data.MinTemp, (double) doc.Descendants("temperature").Attributes("min").First());
        }

        /// <summary>
        ///     call GetWeatherData without location and key - expected exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WeatherDataServiceException))]
        public void GetWeatherDataEmptyParamsTest()
        {
            var service = WeatherDataServiceFactory.GetWeatherDataService(ServiceName.OpenWeatherMap);
            var data = service.GetWeatherData(new Location());
        }
    }
}