using Core.Exceptions;
using Core.Utilities.Results;

namespace Core.ExceptionHandlers;

internal class ValidationExceptionHandlerCommand : IExceptionHandlerCommand
{
    public ExceptionHandlerResult Execute(Exception exception)
    {
        if (exception as ValidationException is var validationException && validationException is null)
            throw new ArgumentException($"The type of the exception is not '{nameof(ValidationException)}'.");

        var result = new ExceptionHandlerResult
        {
            StatusCode = 400,
            Result = Result.Builder().Message(ResultMessages.ErrValidation).Errors(validationException.Errors.ToList()).Build()
        };

        return result;
    }
}