using System.Net;

namespace hero_csharp.Exceptions;

public class ErrorHandleMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandleMiddleware> _logger;

    public ErrorHandleMiddleware(ILogger<ErrorHandleMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try 
        {
            await next(context);
        }
        catch(Exception exception) 
        {
            await HandleErrorAsync(context, exception);
            _logger.LogError(exception, exception.Message);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception exception)
    {
        var errorResponse = MapExceptionToResponse(exception);
        context.Response.StatusCode = errorResponse.StatusCode;
        await context.Response.WriteAsJsonAsync(errorResponse);
    }

    private ErrorResponse MapExceptionToResponse(Exception exception)
    {
        return exception switch
        {
            InvalidOperationException ex => new ErrorResponse { StatusCode = (int) HttpStatusCode.BadRequest, Message = ex.Message },
            _ => new ErrorResponse { StatusCode = (int) HttpStatusCode.InternalServerError, Message = "Something went wrong." }
        };
    }

    private class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } 
    }
}
