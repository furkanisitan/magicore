using MagiCore.Exceptions;
using MagiCore.Results;
using System.Net;

namespace MagiCore.ExceptionHandling.Internal;

internal sealed class ApiClientExceptionHandler : IExceptionHandler<ApiClientException>
{
    public ExceptionHandlerResult Handle(ApiClientException exception)
    {
        return new ExceptionHandlerResult
        {
            StatusCode = HttpStatusCode.InternalServerError,
            Result = Result.Builder().Message(exception.Message).Errors(exception.Errors).Build()
        };
    }
}