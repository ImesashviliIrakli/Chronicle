using Chronicle.Domain.Enums;
using Chronicle.Domain.Errors;
using Chronicle.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Chronicle.Application.Middleware;

public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger = logger;
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = Result.Failure(GlobalStatusCodes.SystemFailure, GlobalErrors.SystemFailure(ex.Message));

            string json = JsonSerializer.Serialize(result);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}
