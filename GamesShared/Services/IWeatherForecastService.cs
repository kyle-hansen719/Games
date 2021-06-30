using System.Collections.Generic;

namespace GamesShared.Services
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> GetWeatherForecasts();
    }
}
