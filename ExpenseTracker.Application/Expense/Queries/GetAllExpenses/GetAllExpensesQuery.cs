using AutoMapper;
using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Shared;
using ExpenseTracker.Shared.DTOs.Expense;
using MediatR;

namespace ExpenseTracker.Application.Expense.Queries.GetAllExpenses;

public record GetAllExpensesQuery() : IRequest<Result<IEnumerable<ExpenseDto>>>;

public class GetAllExpensesQueryHandler : IRequestHandler<GetAllExpensesQuery, Result<IEnumerable<ExpenseDto>>>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public GetAllExpensesQueryHandler(IExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<ExpenseDto>>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var expenses = await _expenseRepository.GetAllAsync();
            var mapedExpense = _mapper.Map<IEnumerable<ExpenseDto>>(expenses);

            return Result<IEnumerable<ExpenseDto>>.Success(mapedExpense);
        }
        catch(Exception ex)
        {
            return Result<IEnumerable<ExpenseDto>>.Failure("Something went wrong");
        }
    }
}
