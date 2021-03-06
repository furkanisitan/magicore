namespace MagiCore.ExceptionHandling;

public interface IExceptionHandler<in T>
    where T : Exception
{
    /// <summary>
    /// Handles the <paramref name="exception"/>.
    /// </summary>
    /// <param name="exception">The exception to handle.</param>
    /// <returns><see cref="ExceptionHandlerResult"/> instance if can be handle <paramref name="exception"/>, otherwise <see langword="null"/>.</returns>
    /// <exception cref="ArgumentException">Throws when type of <paramref name="exception"/> is not of the exception type to handle.</exception>
    ExceptionHandlerResult? Handle(T exception);
}