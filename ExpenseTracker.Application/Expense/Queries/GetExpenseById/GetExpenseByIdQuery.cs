using AutoMapper;
using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Shared;
using ExpenseTracker.Shared.DTOs.Expense;
using MediatR;

namespace ExpenseTracker.Application.Expense.Queries.GetExpenseById;

public record GetExpenseByIdQuery(Guid Id) : IRequest<Result<ExpenseDto>>;

public class GetExpenseByIdQueryHandler : IRequestHandler<GetExpenseByIdQuery, Result<ExpenseDto>>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public GetExpenseByIdQueryHandler(IExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<Result<ExpenseDto>> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var expense = await _expenseRepository.GetByIdAsync(request.Id);
            if(expense == null)
            {
                return Result<ExpenseDto>.Failure("Invalid Expense Id");
            }
            var mappedExpense = _mapper.Map<ExpenseDto>(expense);
            return Result<ExpenseDto>.Success(mappedExpense);
        }
        catch(Exception ex)
        {
            return Result<ExpenseDto>.Failure("Something went wrong");
        }
    }
}
