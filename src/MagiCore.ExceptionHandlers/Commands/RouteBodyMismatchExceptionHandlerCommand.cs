using MagiCore.Exceptions;
using MagiCore.Messaging;
using MagiCore.Results;

namespace MagiCore.ExceptionHandlers.Commands;

internal class RouteBodyMismatchExceptionHandlerCommand : IExceptionHandlerCommand
{
    public ExceptionHandlerResult Execute(Exception exception)
    {
        var ex = Helpers.CheckExceptionType<RouteBodyMismatchException>(exception);

        var result = new ExceptionHandlerResult
        {
            StatusCode = 400,
            Result = Result.Builder().Message(ApiResultMessages.ErrBadRequest).AddError(ex.Message).Build()
        };

        return result;
    }
}