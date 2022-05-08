using MagiCore.Exceptions;
using MagiCore.Results;
using System.Net;

namespace MagiCore.ExceptionHandling.Internal;

internal sealed class ValidationExceptionHandler : IExceptionHandler<ValidationException>
{
    public ExceptionHandlerResult Handle(ValidationException exception)
    {
        var result = new ExceptionHandlerResult
        {
            StatusCode = HttpStatusCode.BadRequest,
            Result = Result.Builder().Message(exception.Message).Errors(exception.Errors).Build()
        };

        return result;
    }
}