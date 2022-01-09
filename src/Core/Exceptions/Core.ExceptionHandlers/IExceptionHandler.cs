namespace Core.ExceptionHandlers;

public interface IExceptionHandler
{
    /// <summary>
    /// Handles the <paramref name="exception"/>.
    /// </summary>
    /// <param name="exception">Exception to handle.</param>
    /// <returns><see cref="ExceptionHandlerResult"/> instance if can be handle <paramref name="exception"/>, otherwise <see langword="null"/>.</returns>
    ExceptionHandlerResult? Handle(Exception exception);
}