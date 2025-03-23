using AutoMapper;
using ExpenseTracker.Application.Contracts;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Shared;
using ExpenseTracker.Shared.DTOs.User;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Xml.Linq;

namespace ExpenseTracker.Application.Services;

public class UserService : Service<User, UserDto, UserCreateDto, UserUpdateDto>, IUserService
{
    private readonly IUserRepository _userRepository;
    private new readonly IMapper _mapper;
    private new readonly ILogger<UserService> _logger;

    public UserService(IUserRepository repository, IMapper mapper, ILogger<UserService> logger) : base(repository, mapper, logger)
    {
        _userRepository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<UserDto?>> GetByEmailAsync(string email)
    {
        try
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return Result<UserDto?>.Failure($"User with email {email} not found.");
            }

            var mappedUser = user != null ? _mapper.Map<UserDto>(user) : null;
            return Result<UserDto?>.Success(mappedUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while fetching User with email {email}.");
            return Result<UserDto?>.Failure($"An error occured while fetching User.");
        }
    }

    public override async Task<Result<UserDto>> CreateAsync(UserCreateDto createDto)
    {
        try
        {
            // Validations
            var validation = await this.Validations(createDto);
            if (!validation.IsSuccess)
                return Result<UserDto>.Failure(validation.Message ?? "An error occured while validating request.");

            // Hash Password
            var hashedPassword = string.IsNullOrEmpty(createDto.Password) ? null : PasswordHasher.HashPassword(createDto.Password);

            // Create a new User Entity
            User entity = new User(createDto.Firstname, createDto.Lastname, createDto.Email, hashedPassword!);

            await _userRepository.AddAsync(entity);
            var response = _mapper.Map<UserDto>(entity);

            return Result<UserDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while creating a user {createDto.Email}.");
            return Result<UserDto>.Failure($"An error occured while creating User.");
        }
    }

    private async Task<Result> Validations(UserCreateDto userCreateDto)
    {
        // Email Validation
        var validEmail = Validation.IsValidEmail(userCreateDto.Email);
        if (!validEmail)
        {
            return Result.Failure("Invalid email format.");
        }

        // Exsisting User Validation
        var existingUser = await _userRepository.GetByEmailAsync(userCreateDto.Email);
        if (existingUser != null)
        {
            return Result.Failure("This User already exists.");
        }

        // Password Validation
        var validPassword = Validation.IsValidPassword(userCreateDto.Password!);
        if (userCreateDto.Password != null && !validPassword)
        {
            return Result.Failure("Password must be at least 6 characters long.");
        }

        return Result.Success();
    }
}
