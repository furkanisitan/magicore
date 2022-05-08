using MagiCore.Exceptions;
using MagiCore.Results;
using System.Net;

namespace MagiCore.ExceptionHandling.Internal;

internal sealed class RecordNotFoundExceptionHandler : IExceptionHandler<RecordNotFoundException>
{
    public ExceptionHandlerResult Handle(RecordNotFoundException exception)
    {
        return new ExceptionHandlerResult
        {
            StatusCode = HttpStatusCode.NotFound,
            Result = Result.Builder().Message(exception.Message).Errors(exception.Errors).Build()
        };
    }
}