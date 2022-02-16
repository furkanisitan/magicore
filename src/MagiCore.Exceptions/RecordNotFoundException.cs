namespace MagiCore.Exceptions;

/// <summary>
/// An exception representing a record not found.
/// </summary>
public class RecordNotFoundException : Exception
{
    public string? Name { get; }
    public (string, object)[] Parameters { get; }

    /// <summary>
    /// Creates a new RecordNotFoundException.
    /// </summary>
    /// <param name="name">The name of the not found record</param>
    /// <param name="parameters">Query parameters key-value pairs</param>
    public RecordNotFoundException(string? name, params (string, object)[] parameters) : base(Helpers.BuildNotFoundMessage(name, parameters))
    {
        Name = name;
        Parameters = parameters;
    }

}