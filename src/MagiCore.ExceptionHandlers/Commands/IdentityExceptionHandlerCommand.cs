using MagiCore.Exceptions;
using MagiCore.Extensions;
using MagiCore.Messaging;
using MagiCore.Results;

namespace MagiCore.ExceptionHandlers.Commands;

internal class IdentityExceptionHandlerCommand : IExceptionHandlerCommand
{
    public ExceptionHandlerResult Execute(Exception exception)
    {
        var ex = exception.Cast<IdentityException>();

        var result = new ExceptionHandlerResult
        {
            StatusCode = 400,
            Result = Result.Builder().Message(ApiResultMessages.ErrIdentity).Errors(ex.Errors.ToList()).Build()
        };

        return result;
    }
}