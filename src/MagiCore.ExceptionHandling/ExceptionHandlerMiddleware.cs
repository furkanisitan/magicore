using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace MagiCore.ExceptionHandling;

/// <summary>
/// A middleware for handling exceptions in the application.
/// </summary>
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            var handler = serviceProvider.GetService(typeof(IExceptionHandler<>).MakeGenericType(e.GetType()));
            var handlerMethod = handler?.GetType().GetMethod(nameof(IExceptionHandler<Exception>.Handle));

            if (handlerMethod?.Invoke(handler, new object?[] { e }) is not ExceptionHandlerResult result) throw;

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)(result.StatusCode ?? HttpStatusCode.InternalServerError);
            await context.Response.WriteAsync(JsonSerializer.Serialize(result.Result));
        }
    }
}