namespace ExpenseTracker.Shared.DTOs.Auth;

public record LoginRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}