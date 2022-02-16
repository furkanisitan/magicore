namespace MagiCore.Exceptions;

/// <summary>
/// An exception that represents failed validation.
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// Validation errors.
    /// </summary>
    public IEnumerable<string> Errors { get; }

    /// <summary>
    /// Creates a new ValidationException.
    /// </summary>
    /// <param name="message"></param>
    public ValidationException(string message) : this(message, Enumerable.Empty<string>())
    {
    }

    /// <summary>
    /// Creates a new ValidationException.
    /// </summary>
    /// <param name="errors"></param>
    public ValidationException(IEnumerable<string> errors) : base(Helpers.BuildErrorMessage(errors, "Validation failed"))
    {
        Errors = errors;
    }

    /// <summary>
    /// Creates a new ValidationException.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="errors"></param>
    public ValidationException(string message, IEnumerable<string> errors) : base(message)
    {
        Errors = errors;
    }

}