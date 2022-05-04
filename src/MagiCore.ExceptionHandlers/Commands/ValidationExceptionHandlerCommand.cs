using MagiCore.Exceptions;
using MagiCore.Extensions;
using MagiCore.Results;

namespace MagiCore.ExceptionHandlers.Commands;

internal class ValidationExceptionHandlerCommand : IExceptionHandlerCommand
{
    public ExceptionHandlerResult Execute(Exception exception)
    {
        var ex = exception.Cast<ValidationException>();

        var result = new ExceptionHandlerResult
        {
            StatusCode = 400,
            Result = Result.Builder().Message(ex.Message).Errors(ex.Errors).Build()
        };

        return result;
    }
}