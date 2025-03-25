using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Shared;
using MediatR;

namespace ExpenseTracker.Application.Expense.Commands.UpdateExpense;

public record UpdateExpenseDto(string? Description, double? Amount, DateTime? Date);

public record UpdateExpenseCommand(Guid Id, string? Description, double? Amount, DateTime? Date) : IRequest<Result>;

public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand, Result>
{
    private readonly IExpenseRepository _expenseRepository;

    public UpdateExpenseCommandHandler(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<Result> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingExpense = await _expenseRepository.GetByIdAsync(request.Id);
            if (existingExpense == null)
                return Result.Failure($"Invalid Expense with ID: {request.Id}");

            existingExpense.UpdateExpense(request.Description, request.Amount, request.Date);
            var response = await _expenseRepository.UpdateAsync(existingExpense);
            return response;
        }
        catch (Exception ex)
        {
            return Result.Failure("Something went wrong");
        }
    }
}