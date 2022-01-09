using Core.Exceptions;

namespace Core.ExceptionHandlers;

public class ExceptionHandler : IExceptionHandler
{
    private readonly IDictionary<Type, IExceptionHandlerCommand> _commands;

    public ExceptionHandler()
    {
        _commands = new Dictionary<Type, IExceptionHandlerCommand>
        {
            { typeof(ValidationException), new ValidationExceptionHandlerCommand() },
        };
    }

    public ExceptionHandlerResult? Handle(Exception exception) =>
        _commands.ContainsKey(exception.GetType()) ? _commands[exception.GetType()].Execute(exception) : null;

}