using AutoMapper;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Shared.DTOs.Category;

namespace ExpenseTracker.Application.Mappings;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>()
            .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name));
    }
}
