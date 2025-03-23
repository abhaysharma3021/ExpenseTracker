namespace ExpenseTracker.Shared.DTOs.User;

public record UserUpdateDto(
    string? Firstname,
    string? Lastname,
    string? Password);