using AutoMapper;

namespace WorkingWithEFCore.MappingProfiles
{
    public class WeatherForecastProfile : Profile
    {
        public WeatherForecastProfile()
        {
            CreateMap<WeatherForecastProfile, WeatherForecastProfile>();
        }
    }
}
