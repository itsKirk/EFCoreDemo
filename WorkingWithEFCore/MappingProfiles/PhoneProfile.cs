using AutoMapper;
using DataLibrary.Models;

namespace WorkingWithEFCore.MappingProfiles
{
    public class PhoneProfile : Profile
    {
        public PhoneProfile()
        {
            CreateMap<Phone, Phone>();
        }
    }
}
