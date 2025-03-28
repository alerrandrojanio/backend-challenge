using Challenge.API.Resources;
using Challenge.Domain.DTOs.Logging;
using Challenge.Domain.Exceptions;
using Challenge.Domain.Interfaces;
using Mapster;
using System.Net;
using System.Text.Json;

namespace Challenge.API.Middlewares;

public class GlobalErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public GlobalErrorHandlerMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlerMiddleware> logger, IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        } 
        catch (Exception ex)
        {
            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
               IMongoDbLogger scopedMongoDbLogger = scope.ServiceProvider.GetRequiredService<IMongoDbLogger>();

               ErrorLogDTO errorLogDTO = ex.Adapt<ErrorLogDTO>();

               await scopedMongoDbLogger.RegisterLog(errorLogDTO);
            }

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
