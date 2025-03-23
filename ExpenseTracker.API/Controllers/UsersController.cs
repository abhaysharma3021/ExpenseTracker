using ExpenseTracker.Application.Contracts;
using ExpenseTracker.Shared;
using ExpenseTracker.Shared.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ExpenseTracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _userService.GetAllAsync();
        if (!result.IsSuccess)
        {
            return NotFound(ApiResponse.Failure(result.Message ?? "An error occurred.", (int)HttpStatusCode.NotFound));
        }

        return Ok(ApiResponse<IEnumerable<UserDto>?>.Success(result.Data, statusCode: (int)HttpStatusCode.OK));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _userService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            return NotFound(ApiResponse.Failure(result.Message ?? "An error occurred", (int)HttpStatusCode.NotFound));
        }

        return Ok(ApiResponse<UserDto?>.Success(result.Data));
    }

    [HttpPost]
    public async Task<IActionResult> Add(UserCreateDto userCreateDto)
    {
        var result = await _userService.CreateAsync(userCreateDto);
        if (!result.IsSuccess)
        {
            return BadRequest(ApiResponse.Failure(result.Message ?? "An error occurred."));
        }

        return CreatedAtAction(nameof(Get), new { id = result.Data!.Id }, ApiResponse<UserDto?>.Success(result.Data, statusCode: (int)HttpStatusCode.Created));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(UserUpdateDto userUpdateDto, Guid id)
    {
        var result = await _userService.UpdateAsync(id, userUpdateDto);
        if (!result.IsSuccess)
        {
            return BadRequest(ApiResponse.Failure(result.Message ?? "An error occurred."));
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _userService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(ApiResponse.Failure(result.Message ?? "An error occurred."));
        }

        return Accepted(ApiResponse.Success("User deleted successfully.", statusCode: (int)HttpStatusCode.Accepted));
    }
}
