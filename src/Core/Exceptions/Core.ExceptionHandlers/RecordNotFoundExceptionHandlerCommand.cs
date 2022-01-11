using Core.Exceptions;
using Core.Utilities.Messaging;
using Core.Utilities.Results;

namespace Core.ExceptionHandlers;

internal class RecordNotFoundExceptionHandlerCommand : IExceptionHandlerCommand
{
    public ExceptionHandlerResult Execute(Exception exception)
    {
        var ex = Helpers.CheckExceptionType<RecordNotFoundException>(exception);

        var result = new ExceptionHandlerResult
        {
            StatusCode = 404,
            Result = Result.Builder().Message(ApiResultMessages.ErrNotFound).AddError(ex.Message).Build()
        };

        return result;
    }
}