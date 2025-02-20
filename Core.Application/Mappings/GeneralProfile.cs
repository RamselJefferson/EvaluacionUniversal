using AutoMapper;
using Core.Application.Dto;
using Core.Application.Request;
using Core.Domain.Entities;


namespace Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {

            // User Mapping
            CreateMap<User, UserDto>()
                .ReverseMap();

            CreateMap<User, UserCreateRequest>()
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<UserDto, UserCreateRequest>()
                .ReverseMap();




        }
    }
}
