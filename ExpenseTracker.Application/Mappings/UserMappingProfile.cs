using AutoMapper;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Shared.DTOs.User;

namespace ExpenseTracker.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        /* Entity -> DTO (Read) */

        // User -> UserDto
        CreateMap<User, UserDto>()
            .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("Email", opt => opt.MapFrom(src => src.Email))
            .ForCtorParam("Name", opt => opt.MapFrom(src => $"{src.Firstname} {src.Lastname}"))
            .ForCtorParam("EmailVerified", opt => opt.MapFrom(src => src.EmailVerified))
            .ForCtorParam("CreatedAt", opt => opt.MapFrom(src => src.CreatedAt))
            .ForCtorParam("UpdatedAt", opt => opt.MapFrom(src => src.UpdatedAt));

        // DTO (Create) -> Entity
        CreateMap<UserCreateDto, User>()
            .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
            .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // DTO (Update) -> Entity
        CreateMap<UserUpdateDto, User>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
