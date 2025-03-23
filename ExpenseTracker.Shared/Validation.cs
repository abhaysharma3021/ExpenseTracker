using System.Text.RegularExpressions;

namespace ExpenseTracker.Shared;

public class Validation
{
    private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
    private static readonly Regex PasswordRegex = new(@"^(?=.*[a-zA-Z])(?=.*[!@#$%^&*()_+\-=\[\]{};:\\|,.<>\/?])?.{6,}$", RegexOptions.Compiled);

    public static bool IsValidEmail(string email) => EmailRegex.IsMatch(email);

    public static bool IsValidPassword(string password) => PasswordRegex.IsMatch(password);
}

