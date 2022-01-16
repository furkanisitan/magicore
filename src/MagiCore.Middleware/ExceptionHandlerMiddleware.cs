﻿using MagiCore.ExceptionHandlers;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace MagiCore.Middleware;

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

    public async Task InvokeAsync(HttpContext context, IExceptionHandler exceptionHandler)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            var result = exceptionHandler.Handle(e);

            if (result is null) throw;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = result.StatusCode ?? 500;
            await context.Response.WriteAsync(JsonSerializer.Serialize(result.Result));
        }
    }
}