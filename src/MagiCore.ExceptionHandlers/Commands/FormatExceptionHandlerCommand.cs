using MagiCore.Messaging;
using MagiCore.Results;

namespace MagiCore.ExceptionHandlers.Commands;

internal class FormatExceptionHandlerCommand : IExceptionHandlerCommand
{
    public ExceptionHandlerResult Execute(Exception exception)
    {
        var ex = Helpers.CheckExceptionType<FormatException>(exception);


        var statusCode = 500;
        var message = ApiResultMessages.ErrInternalServer;


        if (ex.Source is "MongoDB.Bson")
        {
            statusCode = 400;
            message = ApiResultMessages.ErrBadRequest;
        }

        var result = new ExceptionHandlerResult
        {
            StatusCode = statusCode,
            Result = Result.Builder().Message(message).AddError(ex.Message).Build()
        };

        return result;
    }
}