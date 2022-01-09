using System.Text;

namespace Core.Utilities.Results;

public static class ApiResult
{
    public static IResult Ok() =>
        Result.Builder().Success().Message(ResultMessages.Ok).Build();

    public static IResult<T> Ok<T>(T payload) =>
        Result.Builder(payload).Success().Message(ResultMessages.Ok).Build();

    public static IResult<T> Created<T>(T payload) =>
        Result.Builder(payload).Success().Message(ResultMessages.Created).Build();

    public static IResult NotFound(string? name, params KeyValuePair<string, object>[] parameters) =>
        Result.Builder().Message(ResultMessages.ErrNotFound).AddError(BuildNotFoundMessage(name, parameters)).Build();

    private static string BuildNotFoundMessage(string? name, params KeyValuePair<string, object>[] parameters)
    {
        if (name == null) return string.Empty;

        var builder = new StringBuilder(name + " not found");

        if (parameters.Length > 0)
            builder.Append(" for parameters");

        foreach (var (key, value) in parameters)
            builder.Append($" {{{key}='{value}'}}");

        return builder.Append('.').ToString();
    }
}