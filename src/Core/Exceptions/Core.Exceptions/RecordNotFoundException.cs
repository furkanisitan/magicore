namespace Core.Exceptions;

/// <summary>
/// An exception representing a record not found.
/// </summary>
public class RecordNotFoundException : Exception
{
    /// <summary>
    /// Creates a new RecordNotFoundException.
    /// </summary>
    /// <param name="message"></param>
    public RecordNotFoundException(string message) : base(message)
    {
    }
}