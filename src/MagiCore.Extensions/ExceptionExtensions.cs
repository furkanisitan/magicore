namespace MagiCore.Extensions;

/// <summary>
/// This class contains extension methods for the <see cref="Exception"/>.
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// Casts the <paramref name="exception"/> as <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type to cast.</typeparam>
    /// <param name="exception">The exception to be cast.</param>
    /// <returns>An exception cast to <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentException">Throws when type of <paramref name="exception"/> is not <typeparamref name="T"/>.</exception>
    public static T Cast<T>(this Exception exception)
        where T : Exception
    {
        if (exception as T is var ex && ex is null)
            throw new ArgumentException($"The type of the exception is not '{typeof(T)}'.");

        return ex;
    }
}