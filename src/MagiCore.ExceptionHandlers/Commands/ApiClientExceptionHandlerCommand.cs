using MagiCore.Exceptions;
using MagiCore.Messaging;
using MagiCore.Results;

namespace MagiCore.ExceptionHandlers.Commands;

internal class ApiClientExceptionHandlerCommand : IExceptionHandlerCommand
{
    public ExceptionHandlerResult Execute(Exception exception)
    {
        var ex = Helpers.CheckExceptionType<ApiClientException>(exception);

        var result = new ExceptionHandlerResult
        {
            StatusCode = 500,
            Result = Result.Builder().Message(ApiResultMessages.ErrInternalServer).Errors(ex.Errors.ToList()).Build()
        };

        return result;
    }
}