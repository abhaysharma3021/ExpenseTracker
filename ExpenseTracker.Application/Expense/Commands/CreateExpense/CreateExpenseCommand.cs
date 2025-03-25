using ExpenseTracker.Application.Contracts;
using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Shared;
using MediatR;

namespace ExpenseTracker.Application.Expense.Commands.CreateExpense;

public record CreateExpenseCommand(Guid UserId, Guid? CategoryId, string Description, double Amount, DateTime Date) : IRequest<Result>;

public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, Result>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IUserService _userService;
    private readonly ICategoryRepository _categoryRepository;
    public CreateExpenseCommandHandler(IExpenseRepository expenseRepository, IUserService userService, ICategoryRepository categoryRepository)
    {
        _expenseRepository = expenseRepository;
        _userService = userService;
        _categoryRepository = categoryRepository;
    }

    public async Task<Result> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userExists = await _userService.GetByIdAsync(request.UserId);
            if (!userExists.IsSuccess)
            {
                return Result.Failure("User not found.");
            }

            var entity = new Domain.Entities.Expense(request.Description, request.Amount, userExists.Data!.Id, request.Date);

            if(request.CategoryId != null)
            {
                var existingCategory = await _categoryRepository.FindAsync(c => c.Id == request.CategoryId);
                if(existingCategory == null)
                {
                    return Result.Failure($"Invalid Category Id:{request.CategoryId}");
                }

                entity.AddCategory(existingCategory);
            }

            var response = await _expenseRepository.AddAsync(entity);
            return response;
        }
        catch(Exception ex)
        {
            return Result.Failure("Somethign went wrong");
        }
    }
}