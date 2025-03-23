namespace ExpenseTracker.Shared;

public abstract class ApiResponseBase : ResultBase
{
    public int StatusCode { get; }

    protected ApiResponseBase(bool success, string? message, int statusCode) : base(success, message)
    {
        StatusCode = statusCode;
    }
}

public sealed class ApiResponse : ApiResponseBase
{
    private ApiResponse(bool success, string? message, int statusCode) : base(success, message, statusCode) { }

    public static ApiResponse Success(string? message = "Operation succeeded", int statusCode = 200) =>
        new(true, message, statusCode);

    public static ApiResponse Failure(string message, int statusCode = 400) =>
        new(false, message, statusCode);
}

public sealed class ApiResponse<T> : ApiResponseBase
{
    public T? Data { get; }

    private ApiResponse(bool success, T? data, string? message, int statusCode)
        : base(success, message, statusCode)
    {
        Data = data;
    }

    public static ApiResponse<T> Success(T data, string? message = "Operation succeeded", int statusCode = 200) =>
        new(true, data, message, statusCode);

    public static ApiResponse<T> Failure(string message, int statusCode = 400) =>
        new(false, default, message, statusCode);

    public static ApiResponse<T> NoContent(string? message = "No content available", int statusCode = 204) =>
        new(true, default, message, statusCode);
}