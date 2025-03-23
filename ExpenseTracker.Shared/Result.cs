using System.Text.Json.Serialization;

namespace ExpenseTracker.Shared;

public abstract class ResultBase
{
    public bool IsSuccess { get; }
    public string? Message { get; }

    protected ResultBase(bool success, string? message = null)
    {
        IsSuccess = success;
        Message = message;
    }
}

public sealed class Result : ResultBase
{
    private Result(bool success, string? message = null) : base(success, message) { }

    public static Result Success(string? message = null) => new(true, message);
    public static Result Failure(string message) => new(false, message);
}

public sealed class Result<T> : ResultBase
{
    public T? Data { get; }

    [JsonConstructor]
    public Result(bool success, T? data = default, string? message = null) : base(success, message)
    {
        Data = data;
    }
    public static Result<T> Success(T data, string? message = null) => new(true, data, message);
    public static Result<T> Failure(string message) => new(false, default, message);
}