using System.Text;

namespace MagiCore.Exceptions;

internal static class Helpers
{
    /// <summary>
    /// Creates an error message.
    /// </summary>
    /// <param name="errors">Error descriptions.</param>
    /// <param name="prefix">Pre explanation.</param>
    /// <returns></returns>
    public static string BuildErrorMessage(IEnumerable<string> errors, string? prefix = null)
    {
        prefix = prefix is null ? string.Empty : prefix + ": ";

        var arr = errors.Select(x => $"{Environment.NewLine} -- {x}");
        return prefix + string.Join(string.Empty, arr);
    }

    /// <summary>
    /// Creates an error message for not found.
    /// </summary>
    /// <param name="name">The name of the not found record.</param>
    /// <param name="parameters">Query parameters key-value pairs.</param>
    /// <returns></returns>
    public static string BuildNotFoundMessage(string? name, params (string, object)[] parameters)
    {
        var builder = new StringBuilder(name is null ? "Not found" : $"{name} not found");

        if (parameters.Length > 0)
            builder.Append(" for parameters");

        foreach (var (key, value) in parameters)
            builder.Append($" {{{key}='{value}'}}");

        return builder.Append('.').ToString();
    }
}