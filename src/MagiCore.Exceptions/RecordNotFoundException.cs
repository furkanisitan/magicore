using System.Text;

namespace MagiCore.Exceptions;

/// <summary>
/// An exception representing a record not found.
/// </summary>
[Serializable]
public sealed class RecordNotFoundException : Exception
{
    private const string DefaultMessage = "The resource not found.";

    public string? Name { get; }
    public (string, object)[] Parameters { get; }
    public IEnumerable<string> Errors { get; }

    public RecordNotFoundException(string? name, params (string, object)[] parameters)
        : this(DefaultMessage, name, parameters)
    {
    }

    public RecordNotFoundException(string? message, string? name, params (string, object)[] parameters)
        : this(message, null, name, parameters)
    {
    }

    /// <summary>
    /// The constructor of <see cref="RecordNotFoundException"/>.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    /// <param name="name">The name of the not found record.</param>
    /// <param name="parameters">Key-Value pairs that specify query parameters.</param>
    public RecordNotFoundException(string? message, Exception? innerException, string? name, params (string, object)[] parameters)
        : base(message, innerException)
    {
        Name = name;
        Parameters = parameters;
        Errors = Enumerable.Repeat(BuildNotFoundError(name, parameters), 1);
    }

    /// <summary>
    /// Creates an error message with a 'not found' message.
    /// </summary>
    /// <param name="name">The name of the not found record.</param>
    /// <param name="parameters">Key-Value pairs that specify query parameters.</param>
    /// <returns>A <see cref="string"/> with a 'not found' message.</returns>
    private static string BuildNotFoundError(string? name, params (string, object)[] parameters)
    {
        if (parameters.Length == 0)
            return name is null ? "Not found." : $"{name} not found.";

        var builder = new StringBuilder(name is null ? "Not found for parameters" : $"{name} not found for parameters");

        foreach (var (key, value) in parameters)
            builder.Append($" {{{key}='{value}'}}");

        return builder.Append('.').ToString();
    }

}