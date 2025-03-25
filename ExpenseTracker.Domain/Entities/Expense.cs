namespace ExpenseTracker.Domain.Entities;

public class Expense
{
    public Expense(string description, double amount, Guid userId, DateTime date)
    {
        Id = Guid.NewGuid();
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Amount = amount;
        Date = date;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

    public Guid UserId {  get; private set; }
    public User User { get; private set; }

    public Guid? CategoryId { get; private set; }
    public Category? Category { get; private set; }

    public string Description { get; private set; } = string.Empty;
    public double Amount { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public void UpdateExpense(string? description, double? amount, DateTime? date)
    {
        if(!string.IsNullOrEmpty(description))
            Description = description ?? throw new ArgumentNullException(nameof(description));

        if(amount != null)
            Amount = amount.Value;

        if(date != null)
            Date = date.Value;

        UpdatedAt = DateTime.UtcNow;
    }

    public void AddCategory(Category category)
    {
        CategoryId = category.Id;
        Category = category;
    }
}
