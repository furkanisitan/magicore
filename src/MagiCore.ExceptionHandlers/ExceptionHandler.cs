﻿using MagiCore.ExceptionHandlers.Commands;
using MagiCore.Exceptions;

namespace MagiCore.ExceptionHandlers;

public class ExceptionHandler : IExceptionHandler
{
    private readonly IDictionary<Type, IExceptionHandlerCommand> _commands;

    public ExceptionHandler()
    {
        _commands = new Dictionary<Type, IExceptionHandlerCommand>
        {
            { typeof(ValidationException), new ValidationExceptionHandlerCommand() },
            { typeof(RecordNotFoundException), new RecordNotFoundExceptionHandlerCommand() },
            { typeof(RouteBodyMismatchException), new RouteBodyMismatchExceptionHandlerCommand() }
        };
    }

    public ExceptionHandlerResult? Handle(Exception exception) =>
        _commands.ContainsKey(exception.GetType()) ? _commands[exception.GetType()].Execute(exception) : null;

}