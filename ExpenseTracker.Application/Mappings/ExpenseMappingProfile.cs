using AutoMapper;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Shared.DTOs.Expense;

namespace ExpenseTracker.Application.Mappings;

public class ExpenseMappingProfile : Profile
{
    public ExpenseMappingProfile()
    {
        // Expense -> ExpenseDto
        CreateMap<Domain.Entities.Expense, ExpenseDto>()
            .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("User", opt => opt.MapFrom(src => src.User.Email))
            .ForCtorParam("Category", opt => opt.MapFrom(src => src.Category.Name))
            .ForCtorParam("Description", opt => opt.MapFrom(src => src.Description))
            .ForCtorParam("Amount", opt => opt.MapFrom(src => src.Amount))
            .ForCtorParam("Date", opt => opt.MapFrom(src => src.Date));
    }
}
