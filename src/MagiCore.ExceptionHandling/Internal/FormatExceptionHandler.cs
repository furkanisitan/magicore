using MagiCore.Results;
using System.Net;

namespace MagiCore.ExceptionHandling.Internal;

internal sealed class FormatExceptionHandler : IExceptionHandler<FormatException>
{
    public ExceptionHandlerResult Handle(FormatException exception)
    {
        if (exception.Source is "MongoDB.Bson")
            return new ExceptionHandlerResult
            {
                StatusCode = HttpStatusCode.BadRequest,
                Result = Result.Builder().Message(Messages.BadRequest).Errors(exception.Message).Build()
            };

        return new ExceptionHandlerResult
        {
            StatusCode = HttpStatusCode.InternalServerError,
            Result = Result.Builder().Message(Messages.InternalServer).Errors(exception.Message).Build()
        };
    }
}