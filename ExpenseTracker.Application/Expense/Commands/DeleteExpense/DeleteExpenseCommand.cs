using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Shared;
using MediatR;

namespace ExpenseTracker.Application.Expense.Commands.DeleteExpense;

public record DeleteExpenseCommand(Guid Id) : IRequest<Result>;

public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, Result>
{
    private readonly IExpenseRepository _expenseRepository;
    public DeleteExpenseCommandHandler(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<Result> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingExpense = await _expenseRepository.GetByIdAsync(request.Id);
            if (existingExpense == null)
                return Result.Failure($"Invalid Expense with ID: {request.Id}");

            var response = await _expenseRepository.DeleteAsync(request.Id);
            return response;
        }
        catch (Exception ex) 
        {
            return Result.Failure("Something went wrong");
        }
    }
}