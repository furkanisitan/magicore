﻿using MagiCore.Exceptions;
using MagiCore.Extensions;
using MagiCore.Results;

namespace MagiCore.ExceptionHandlers.Commands;

internal class ApiClientExceptionHandlerCommand : IExceptionHandlerCommand
{
    public ExceptionHandlerResult Execute(Exception exception)
    {
        var ex = exception.Cast<ApiClientException>();

        return new ExceptionHandlerResult
        {
            StatusCode = 500,
            Result = Result.Builder().Message(ex.Message).Errors(ex.Errors).Build()
        };
    }
}