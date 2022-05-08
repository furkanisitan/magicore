using MagiCore.Exceptions;
using MagiCore.Results;
using System.Net;

namespace MagiCore.ExceptionHandling.Internal;

internal sealed class IdentityExceptionHandler : IExceptionHandler<IdentityException>
{
    public ExceptionHandlerResult Handle(IdentityException exception)
    {
        return new ExceptionHandlerResult
        {
            StatusCode = HttpStatusCode.BadRequest,
            Result = Result.Builder().Message(exception.Message).Errors(exception.Errors.ToList()).Build()
        };
    }
}