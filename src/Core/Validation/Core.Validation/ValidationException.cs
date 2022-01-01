namespace Core.Validation;

/// <summary>
/// An exception that represents failed validation.
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// Creates a new ValidationException
    /// </summary>
    /// <param name="message"></param>
    public ValidationException(string message) : base(message)
    {
    }
}