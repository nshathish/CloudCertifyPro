using Api.EndPoints.Authentication.Dtos;
using AutoMapper;
using Data.Account;

namespace Api.EndPoints.Authentication.Mapping;

public class AuthenticationMapping : Profile
{
    public AuthenticationMapping()
    {
        CreateMap<UserRegistrationDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
    }
}