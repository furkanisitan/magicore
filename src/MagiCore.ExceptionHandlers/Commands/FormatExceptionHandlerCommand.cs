using MagiCore.Extensions;
using MagiCore.Results;

namespace MagiCore.ExceptionHandlers.Commands;

internal class FormatExceptionHandlerCommand : IExceptionHandlerCommand
{
    public ExceptionHandlerResult Execute(Exception exception)
    {
        var ex = exception.Cast<FormatException>();

        if (ex.Source is "MongoDB.Bson")
            return new ExceptionHandlerResult
            {
                StatusCode = 400,
                Result = Result.Builder().Message(Messages.BadRequest).AddError(ex.Message).Build()
            };

        return new ExceptionHandlerResult
        {
            StatusCode = 500,
            Result = Result.Builder().Message(Messages.InternalServer).AddError(ex.Message).Build()
        };
    }
}