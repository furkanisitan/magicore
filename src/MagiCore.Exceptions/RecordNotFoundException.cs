using System.Text;

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
    public RecordNotFoundException(string? name, params (string, object)[] parameters) : base(BuildNotFoundMessage(name, parameters))
    {
        Name = name;
        Parameters = parameters;
    }

    private static string BuildNotFoundMessage(string? name, params (string, object)[] parameters)
    {
        var builder = new StringBuilder(name is null ? "Not found" : $"{name} not found");

        if (parameters.Length > 0)
            builder.Append(" for parameters");

        foreach (var (key, value) in parameters)
            builder.Append($" {{{key}='{value}'}}");

        return builder.Append('.').ToString();
    }
}