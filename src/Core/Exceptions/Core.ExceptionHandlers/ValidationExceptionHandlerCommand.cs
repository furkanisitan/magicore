using Core.Exceptions;
using Core.Utilities.Messaging;
using Core.Utilities.Results;

namespace Core.ExceptionHandlers;

internal class ValidationExceptionHandlerCommand : IExceptionHandlerCommand
{
    public ExceptionHandlerResult Execute(Exception exception)
    {
        var ex = Helpers.CheckExceptionType<ValidationException>(exception);

        var result = new ExceptionHandlerResult
        {
            StatusCode = 400,
            Result = Result.Builder().Message(ApiResultMessages.ErrValidation).Errors(ex.Errors.ToList()).Build()
        };

        return result;
    }
}