namespace ExpenseTracker.Shared.DTOs.Auth;

public record RegisterUserDto(
    string Email,
    string Firstname,
    string Lastname,
    string Password);