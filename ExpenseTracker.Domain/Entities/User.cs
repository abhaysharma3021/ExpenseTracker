using ExpenseTracker.Shared;

namespace ExpenseTracker.Domain.Entities;

public class User
{
    private User() { } // Required for EF Core

    public User(string firstname, string lastname, string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || !Validation.IsValidEmail(email))
            throw new ArgumentException("Invalid email format.", nameof(email));

        if (password != null && !Validation.IsValidPassword(password))
            throw new ArgumentException("Password must be at least 6 characters long.", nameof(password));

        Id = Guid.NewGuid();
        Email = email;
        Firstname = firstname ?? throw new ArgumentNullException(nameof(firstname));
        Lastname = lastname ?? throw new ArgumentNullException(nameof(lastname));
        Password = password!;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public string Firstname { get; private set; }
    public string Lastname { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public DateTime? EmailVerified { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
}
