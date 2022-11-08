
using AutoMapper;
using CompanyEmployees.Entities.DataTransferObjects;
using Entities.Models;

namespace Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(c => c.FullAddress,
                    opt => opt.MapFrom(x => string.Join(x.Address, x.Country)));

           /* CreateMap<UserForRegistrationDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));*/
        }
    }
}
