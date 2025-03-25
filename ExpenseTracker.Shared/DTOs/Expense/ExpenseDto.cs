namespace ExpenseTracker.Shared.DTOs.Expense;

public record ExpenseDto(Guid Id, string User, string Category, string Description, double Amount, DateTime Date);