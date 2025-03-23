using ExpenseTracker.Shared.DTOs.User;

namespace ExpenseTracker.Shared.DTOs.Auth;

public record LoginResponseDto(
    string? access_token,
    UserDto? user
);