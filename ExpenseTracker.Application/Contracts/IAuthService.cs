using ExpenseTracker.Shared.DTOs.Auth;
using ExpenseTracker.Shared.DTOs.User;
using ExpenseTracker.Shared;

namespace ExpenseTracker.Application.Contracts;

public interface IAuthService
{
    Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto);
    Task<Result<UserDto>> RegisterAsync(RegisterUserDto registerUserDto);
}
