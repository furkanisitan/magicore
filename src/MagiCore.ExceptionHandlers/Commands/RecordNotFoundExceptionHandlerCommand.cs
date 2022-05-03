using MagiCore.Exceptions;
using MagiCore.Extensions;
using MagiCore.Messaging;
using MagiCore.Results;

namespace MagiCore.ExceptionHandlers.Commands;

internal class RecordNotFoundExceptionHandlerCommand : IExceptionHandlerCommand
{
    public ExceptionHandlerResult Execute(Exception exception)
    {
        var ex = exception.Cast<RecordNotFoundException>();

        var result = new ExceptionHandlerResult
        {
            StatusCode = 404,
            Result = Result.Builder().Message(ApiResultMessages.ErrNotFound).AddError(ex.Message).Build()
        };

        return result;
    }
}