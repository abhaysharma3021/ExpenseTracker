using ExpenseTracker.Application.Contracts;
using ExpenseTracker.Shared.DTOs.Auth;
using ExpenseTracker.Shared.DTOs.User;
using ExpenseTracker.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ExpenseTracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
    {

        var result = await _authService.RegisterAsync(registerUserDto);

        if (!result.IsSuccess)
        {
            return BadRequest(ApiResponse.Failure(result.Message!));
        }

        return CreatedAtAction(nameof(Login), new { id = result.Data!.Id }, ApiResponse<UserDto?>.Success(result.Data, statusCode: (int)HttpStatusCode.Created));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto loginRequest)
    {
        var result = await _authService.LoginAsync(loginRequest);

        if (!result.IsSuccess)
        {
            return BadRequest(ApiResponse.Failure(result.Message!));
        }

        return Ok(ApiResponse<LoginResponseDto>.Success(result.Data!));
    }
}
