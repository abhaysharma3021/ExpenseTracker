using AutoMapper;
using ExpenseTracker.Application.Contracts;
using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Shared;
using ExpenseTracker.Shared.DTOs.Auth;
using ExpenseTracker.Shared.DTOs.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExpenseTracker.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public AuthService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration, IUserService userService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration;
        _userService = userService;
    }

    public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto)
    {
        try
        {
            var existingUser = await _userRepository.GetByEmailAsync(loginRequestDto.Email);
            if(existingUser == null || !PasswordHasher.VerifyPassword(loginRequestDto.Password, existingUser.Password ?? ""))
            {
                return Result<LoginResponseDto>.Failure("Invalid credentials");
            }

            if (existingUser.EmailVerified == null)
            {
                /* 1. Generate Verification token */

                /* 2. Send Verify Mail Directly OR Generate message to Publish on RabbitMQ */

                //return Result<LoginResponseDto>.Failure("Email not verified. Check your mail to verify your account");
            }

            var mapToDto = _mapper.Map<UserDto>(existingUser);
            var token = await GenerateTokenAsync(mapToDto);

            return Result<LoginResponseDto>.Success(new LoginResponseDto(token, mapToDto));
        }
        catch(Exception ex)
        {
            return Result<LoginResponseDto>.Failure("An internal server error occurred. Please try again later.");
        }
    }

    public async Task<Result<UserDto>> RegisterAsync(RegisterUserDto registerUserDto)
    {
        try
        {
            var request = new UserCreateDto(registerUserDto.Email, registerUserDto.Firstname, registerUserDto.Lastname, registerUserDto.Password);
            var response = await _userService.CreateAsync(request);

            // Send verification email
            if (response.IsSuccess)
            {
                /* 1. Generate verification token */

                /* 2. Send Verify Mail Directly OR Generate message to Publish on RabbitMQ */
            }
            return response;
        }
        catch(Exception ex)
        {
            return Result<UserDto>.Failure("An error occured while registering the user");
        }
        throw new NotImplementedException();
    }

    private async Task<string> GenerateTokenAsync(UserDto userDto)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = Convert.FromBase64String(jwtSettings["Key"]!.ToString());

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userDto.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, userDto.Email),
            new(JwtRegisteredClaimNames.Name, userDto.Name)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(60 * 24 * 2),
            Issuer = jwtSettings["Issuer"]!.ToString(),
            Audience = jwtSettings["Audience"]!.ToString(),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return await Task.FromResult(tokenHandler.WriteToken(token));
    }
}
