namespace MagiCore.Exceptions;

/// <summary>
/// An exception representing an API client credential error.
/// </summary>
public class ApiClientException : Exception
{
    /// <summary>
    /// Identity errors.
    /// </summary>
    public IEnumerable<string> Errors { get; }

    /// <summary>
    /// Creates a new ApiClientCredentialException.
    /// </summary>
    /// <param name="errors"></param>
    public ApiClientException(IEnumerable<string> errors) : base(Helpers.BuildErrorMessage(errors, "API client credential authentication failed"))
    {
        Errors = errors;
    }

    /// <summary>
    /// Creates a new ApiClientCredentialException.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="errors"></param>
    public ApiClientException(string message, IEnumerable<string> errors) : base(message)
    {
        Errors = errors;
    }
}