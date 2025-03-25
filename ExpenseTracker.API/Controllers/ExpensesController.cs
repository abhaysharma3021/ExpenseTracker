using ExpenseTracker.Application.Expense.Commands.CreateExpense;
using ExpenseTracker.Application.Expense.Commands.DeleteExpense;
using ExpenseTracker.Application.Expense.Commands.UpdateExpense;
using ExpenseTracker.Application.Expense.Queries.GetAllExpenses;
using ExpenseTracker.Application.Expense.Queries.GetExpenseById;
using ExpenseTracker.Application.Expense.Queries.GetFilteredExpenses;
using ExpenseTracker.Shared;
using ExpenseTracker.Shared.DTOs.Expense;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ExpenseTracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpensesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllExpensesQuery());
        if (!result.IsSuccess)
        {
            return NotFound(ApiResponse.Failure(result.Message ?? "An error occurred.", (int)HttpStatusCode.NotFound));
        }

        return Ok(ApiResponse<IEnumerable<ExpenseDto>?>.Success(result.Data, statusCode: (int)HttpStatusCode.OK));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new GetExpenseByIdQuery(id));
        if (!result.IsSuccess)
        {
            return NotFound(ApiResponse.Failure(result.Message ?? "An error occurred.", (int)HttpStatusCode.NotFound));
        }

        return Ok(ApiResponse<ExpenseDto?>.Success(result.Data, statusCode: (int)HttpStatusCode.OK));
    }

    [HttpGet("{userid:guid}/filtered")]
    public async Task<IActionResult> GetFiltered(Guid userid, DateTime? startDate, DateTime? endDate)
    {
        var result = await _mediator.Send(new GetFilteredExpensesQuery(userid, startDate, endDate));
        if (!result.IsSuccess)
        {
            return NotFound(ApiResponse.Failure(result.Message ?? "An error occurred.", (int)HttpStatusCode.NotFound));
        }

        return Ok(ApiResponse<IEnumerable<ExpenseDto>>.Success(result.Data!, statusCode: (int)HttpStatusCode.OK));
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateExpenseCommand request)
    {
        var result = await _mediator.Send(request);
        if (!result.IsSuccess)
        {
            return BadRequest(ApiResponse.Failure(result.Message ?? "An error occurred.", (int)HttpStatusCode.NotFound));
        }

        return Ok(ApiResponse.Success(statusCode: (int)HttpStatusCode.OK));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateExpenseDto request)
    {
        var result = await _mediator.Send(new UpdateExpenseCommand(id, request.Description, request.Amount, request.Date));
        if (!result.IsSuccess)
        {
            return BadRequest(ApiResponse.Failure(result.Message ?? "An error occurred.", (int)HttpStatusCode.NotFound));
        }

        return Ok(ApiResponse.Success(statusCode: (int)HttpStatusCode.OK));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteExpenseCommand(id));
        if (!result.IsSuccess)
        {
            return BadRequest(ApiResponse.Failure(result.Message ?? "An error occurred.", (int)HttpStatusCode.NotFound));
        }

        return Ok(ApiResponse.Success(statusCode: (int)HttpStatusCode.OK));
    }
}
