using System.Collections.Generic;
using Ketum.Web.Models;

namespace Ketum.Web.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
