namespace ExpenseTracker.Shared.DTOs.User;

public record UserCreateDto(
    string Email,
    string Firstname,
    string Lastname,
    string Password);