using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace AngularProject.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
    HttpContext context,
    Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "An unexpected error occurred.";
        string? details = null;

        switch (exception)
        {
            case ArgumentException:
                statusCode = HttpStatusCode.BadRequest;
                message = exception.Message;
                break;

            case KeyNotFoundException:
                statusCode = HttpStatusCode.NotFound;
                message = exception.Message;
                break;

            case DbUpdateException dbEx
                when dbEx.InnerException?.Message.Contains("UNIQUE") == true:

                statusCode = HttpStatusCode.BadRequest;
                message = "Email already exists.";
                break;

            case UnauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized;
                message = "Unauthorized access.";
                break;
        }

        // Show stack trace only in Development
        if (context.RequestServices
            .GetService<IWebHostEnvironment>()?
            .IsDevelopment() == true)
        {
            details = exception.StackTrace;
        }

        var response = new ErrorResponse
        {
            StatusCode = (int)statusCode,
            Message = message,
            Details = details,
            Timestamp = DateTime.UtcNow
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.StatusCode;

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }

}
