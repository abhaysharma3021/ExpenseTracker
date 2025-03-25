using FluentValidation;

namespace ExpenseTracker.Application.Expense.Commands.UpdateExpense;

public class UpdateExpenseCommandValidator : AbstractValidator<UpdateExpenseCommand>
{
    public UpdateExpenseCommandValidator()
    {
        RuleFor(v => v.Description)
            .MaximumLength(100);

        RuleFor(v => v.Amount)
            .GreaterThanOrEqualTo(0);
    }
}
