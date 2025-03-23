using ExpenseTracker.Shared;
using ExpenseTracker.Shared.DTOs.User;

namespace ExpenseTracker.Application.Contracts;

public interface IUserService : IService<UserDto, UserCreateDto, UserUpdateDto>
{
    Task<Result<UserDto?>> GetByEmailAsync(string email);
}
