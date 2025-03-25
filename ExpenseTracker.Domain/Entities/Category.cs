namespace ExpenseTracker.Domain.Entities;

public class Category
{
    private readonly List<Expense> _expenses = new();

    private Category() { } // Required of EF Core

    public Category(string name)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public ICollection<Expense> Expense
    {
        get => _expenses;
        private set => _expenses.Clear(); // Prevents EF from replacing the collection
    }
}
