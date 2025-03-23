namespace ExpenseTracker.Shared.DTOs.User;

public record UserDto(
    Guid Id,
    string Email,
    string Name,
    DateTime? EmailVerified,
    DateTime CreatedAt,
    DateTime UpdatedAt);