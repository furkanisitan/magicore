namespace MagiCore.Exceptions;

/// <summary>
/// An exception representing an identity error.
/// </summary>
public class IdentityException : Exception
{
    /// <summary>
    /// Identity errors.
    /// </summary>
    public IEnumerable<string> Errors { get; }

    /// <summary>
    /// Creates a new IdentityException.
    /// </summary>
    /// <param name="errors"></param>
    public IdentityException(IEnumerable<string> errors) : base(Helpers.BuildErrorMessage(errors, "Identity failed"))
    {
        Errors = errors;
    }

    /// <summary>
    /// Creates a new IdentityException.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="errors"></param>
    public IdentityException(string message, IEnumerable<string> errors) : base(message)
    {
        Errors = errors;
    }

}