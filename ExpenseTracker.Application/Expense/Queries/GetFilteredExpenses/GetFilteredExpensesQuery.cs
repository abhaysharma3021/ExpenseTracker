using ExpenseTracker.Shared.DTOs.Expense;
using ExpenseTracker.Shared;
using MediatR;
using ExpenseTracker.Domain.Interfaces;

namespace ExpenseTracker.Application.Expense.Queries.GetFilteredExpenses;

public record GetFilteredExpensesQuery(
    Guid UserId,
    DateTime? StartDate,
    DateTime? EndDate) : IRequest<Result<IEnumerable<ExpenseDto>>>;

public class GetFilteredExpensesQueryHandler : IRequestHandler<GetFilteredExpensesQuery, Result<IEnumerable<ExpenseDto>>>
{
    private readonly IExpenseRepository _expenseRepository;

    public GetFilteredExpensesQueryHandler(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<Result<IEnumerable<ExpenseDto>>> Handle(GetFilteredExpensesQuery request, CancellationToken cancellationToken)
    {
        // Get expenses for the user with optional filters
        var expenses = await _expenseRepository.FindAllAsync(
            e => e.UserId == request.UserId &&
                 (!request.StartDate.HasValue || e.Date >= request.StartDate.Value) &&
                 (!request.EndDate.HasValue || e.Date <= request.EndDate.Value));

        var expenseDtos = expenses
            .OrderByDescending(e => e.Date)
            .Select(e => new ExpenseDto(e.Id, e.User.Email, e.Category != null ? e.Category.Name : "Uncategorized", e.Description, e.Amount, e.Date))
            .ToList();

        return Result<IEnumerable<ExpenseDto>>.Success(expenseDtos);
    }
}