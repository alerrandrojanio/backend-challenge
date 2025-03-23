using Challenge.API.Resources;
using Challenge.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace Challenge.API.Middlewares;

public class GlobalErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;

    public GlobalErrorHandlerMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        } 
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado: {Message}", ex.Message);

            if (ex is ServiceException serviceException)
                await HandleExceptionAsync(context, serviceException);
            else
                await HandleExceptionAsync(context);
          
            if (!context.Response.HasStarted)
                throw;
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, ServiceException? exception = null)
    {
        HttpStatusCode statusCode = exception is not null ? exception.StatusCode : HttpStatusCode.InternalServerError;
        string message = exception is not null ? exception.Message : ResourceMsg.Error_GenericError;

        var errorResponse = new
        {
            StatusCode = (int)statusCode,
            Message = message
        };

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        string result = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(result);
    }
}
