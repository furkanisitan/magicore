namespace MagiCore.Exceptions;

/// <summary>
/// An exception that represents failed validation.
/// </summary>
public class ValidationException : Exception
{
    private const string DefaultMessage = "A validation error has occurred.";

    public IEnumerable<string> Errors { get; }

    public ValidationException(IEnumerable<string>? errors)
        : this(DefaultMessage, errors)
    {
    }

    public ValidationException(string? message, IEnumerable<string>? errors)
        : this(message, null, errors)
    {
    }

    /// <summary>
    /// The constructor of <see cref="ValidationException"/>.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    /// <param name="errors"></param>
    public ValidationException(string? message, Exception? innerException, IEnumerable<string>? errors)
        : base(message, innerException)
    {
        Errors = errors ?? Enumerable.Empty<string>();
    }

}