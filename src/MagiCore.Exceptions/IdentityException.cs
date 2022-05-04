namespace MagiCore.Exceptions;

/// <summary>
/// An exception representing an identity error.
/// </summary>
[Serializable]
public sealed class IdentityException : Exception
{
    private const string DefaultMessage = "The credentials are invalid.";

    public IEnumerable<string> Errors { get; }

    public IdentityException(IEnumerable<string>? errors)
        : this(DefaultMessage, errors)
    {
    }

    public IdentityException(string? message, IEnumerable<string>? errors)
        : this(message, null, errors)
    {
    }

    /// <summary>
    /// The constructor of <see cref="IdentityException"/>.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    /// <param name="errors"></param>
    public IdentityException(string? message, Exception? innerException, IEnumerable<string>? errors)
        : base(message, innerException)
    {
        Errors = errors ?? Enumerable.Empty<string>();
    }

}