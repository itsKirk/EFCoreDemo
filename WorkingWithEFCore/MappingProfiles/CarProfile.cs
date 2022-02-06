using AutoMapper;
using DataLibrary.Models;

namespace WorkingWithEFCore.MappingProfiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, Car>();
        }
    }
}
