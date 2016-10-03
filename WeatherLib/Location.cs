using System;

namespace WeatherLib
{
    public enum LocationType
    {
        ZipCode,
        Name,
        Coord
    }

    /// <summary>
    ///     Location reprsents all properties relevant for any location type
    /// </summary>
    public class Location
    {
        /// <summary>
        ///     city constructor
        /// </summary>
        /// <param name="name">city name</param>
        public Location(string name)
        {
            Name = name;
        }

        /// <summary>
        ///     zipCode constructor
        /// </summary>
        /// <param name="zipCode">zip number</param>
        public Location(int zipCode)
        {
            ZipCode = zipCode;
        }

        /// <summary>
        ///     geographics coord constructor
        /// </summary>
        /// <param name="lon">longitude</param>
        /// <param name="lat">latitude</param>
        public Location(double lon, double lat)
        {
            Lon = lon;
            Lat = lat;
        }

        public Location()
        {
        }

        public int ZipCode { set; get; }
        public string Name { set; get; }
        public double Lon { get; set; }
        public double Lat { get; set; }
        public DateTime SunRise { get; set; }
        public DateTime SunSet { get; set; }

        /// <summary>
        ///     returns the location type that initialized
        /// </summary>
        /// <returns></returns>
        public LocationType GetLocationType()
        {
            if (Name != null) return LocationType.Name;
            if (ZipCode != 0) return LocationType.ZipCode;
            if ((Lon != 0) && (Lat != 0)) return LocationType.Coord;
            throw new WeatherDataServiceException("Location type exception");
        }
    }
}