using System;

namespace WeatherLib
{
    /// <summary>
    ///     Exception class libary
    /// </summary>
    public class WeatherDataServiceException : Exception
    {
        public WeatherDataServiceException(string msg) : base(msg)
        {
        }
    }
}