using FluentValidation;

namespace ExpenseTracker.Application.Expense.Commands.CreateExpense;

public class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommand>
{
    public CreateExpenseCommandValidator()
    {
        RuleFor(v => v.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(100);

        RuleFor(v => v.Amount)
            .NotEmpty().WithMessage("Amount is required")
            .GreaterThanOrEqualTo(0);

        RuleFor(v => v.Date)
            .NotEmpty().WithMessage("Date is required");
    }
}
