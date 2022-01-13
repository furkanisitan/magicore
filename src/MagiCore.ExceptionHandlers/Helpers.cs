namespace MagiCore.ExceptionHandlers;

internal static class Helpers
{
    /// <summary>
    /// Checks the type of <paramref name="exception"/>.
    /// </summary>
    /// <typeparam name="T">Exception type to check.</typeparam>
    /// <param name="exception">Exception to check.</param>
    /// <returns><paramref name="exception"/> cast to <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentException">Throws when type of <paramref name="exception"/> is not <typeparamref name="T"/>.</exception>
    public static T CheckExceptionType<T>(Exception exception)
        where T : Exception
    {
        if (exception as T is var ex && ex is null)
            throw new ArgumentException($"The type of the exception is not '{typeof(T)}'.");

        return ex;
    }
}