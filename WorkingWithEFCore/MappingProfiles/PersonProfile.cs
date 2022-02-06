using AutoMapper;
using DataLibrary.Models;

namespace WorkingWithEFCore.MappingProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, Person>();
        }
    }
}
