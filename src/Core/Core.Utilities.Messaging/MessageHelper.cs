using System.Text;

namespace Core.Utilities.Messaging;

public static class MessageHelper
{
    public static string BuildNotFoundMessage(string? name, params KeyValuePair<string, object>[] parameters)
    {
        var builder = new StringBuilder(name is null ? "Not found" : $"{name} not found");

        if (parameters.Length > 0)
            builder.Append(" for parameters");

        foreach (var (key, value) in parameters)
            builder.Append($" {{{key}='{value}'}}");

        return builder.Append('.').ToString();
    }
}