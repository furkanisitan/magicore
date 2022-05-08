using MagiCore.Exceptions;
using MagiCore.Results;
using System.Net;

namespace MagiCore.ExceptionHandling.Internal;

internal sealed class RouteBodyMismatchExceptionHandler : IExceptionHandler<RouteBodyMismatchException>
{
    public ExceptionHandlerResult Handle(RouteBodyMismatchException exception)
    {
        var result = new ExceptionHandlerResult
        {
            StatusCode = HttpStatusCode.BadRequest,
            Result = Result.Builder().Message(exception.Message).Errors(exception.Errors).Build()
        };

        return result;
    }
}