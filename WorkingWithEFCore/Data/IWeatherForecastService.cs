using DataLibrary.Models;

namespace WorkingWithEFCore.Data
{
    public interface IWeatherForecastService
    {
        WeatherForecast GetForecast(DateTime date);
    }
}