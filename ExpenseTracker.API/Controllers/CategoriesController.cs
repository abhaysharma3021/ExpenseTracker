using ExpenseTracker.Application.Categories.Commands.CreateCategory;
using ExpenseTracker.Application.Categories.Queries.GetAllCategories;
using ExpenseTracker.Application.Categories.Queries.GetCategoryById;
using ExpenseTracker.Shared;
using ExpenseTracker.Shared.DTOs.Category;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ExpenseTracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllCategoriesQuery());
        if (!result.IsSuccess)
        {
            return NotFound(ApiResponse.Failure(result.Message ?? "An error occurred.", (int)HttpStatusCode.NotFound));
        }

        return Ok(ApiResponse<IEnumerable<CategoryDto>?>.Success(result.Data, statusCode: (int)HttpStatusCode.OK));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new GetCategoryByIdQuery(id));
        if (!result.IsSuccess)
        {
            return NotFound(ApiResponse.Failure(result.Message ?? "An error occurred.", (int)HttpStatusCode.NotFound));
        }

        return Ok(ApiResponse<CategoryDto?>.Success(result.Data, statusCode: (int)HttpStatusCode.OK));
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(CreateCategoryCommand request)
    {
        var result = await _mediator.Send(request);
        if (!result.IsSuccess)
        {
            return BadRequest(ApiResponse.Failure(result.Message ?? "An error occurred.", (int)HttpStatusCode.BadRequest));
        }

        return Ok(ApiResponse.Success(statusCode: (int)HttpStatusCode.OK));
    }
}
