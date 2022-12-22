using AutoMapper;
using TimeTracking.Domain;
using TimeTracking.Domain.DataTransferObjects;

namespace TimeTracking.Service.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<User, UserLoginDto>();
        CreateMap<User, UserRegisterDto>();
        CreateMap<UserRegisterDto, User>();
        CreateMap<UserLoginDto, User>();
        CreateMap<UserDto, User>();
    }
}
