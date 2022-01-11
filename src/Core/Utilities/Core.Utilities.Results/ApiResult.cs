using Core.Utilities.Messaging;

namespace Core.Utilities.Results;

public static class ApiResult
{
    public static IResult Ok() =>
        Result.Builder().Success().Message(ApiResultMessages.Ok).Build();

    public static IResult<T> Ok<T>(T payload) =>
        Result.Builder(payload).Success().Message(ApiResultMessages.Ok).Build();

    public static IResult<T> Created<T>(T payload) =>
        Result.Builder(payload).Success().Message(ApiResultMessages.Created).Build();

    public static IResult NotFound(string? name, params KeyValuePair<string, object>[] parameters) =>
        Result.Builder().Message(ApiResultMessages.ErrNotFound).AddError(MessageHelper.BuildNotFoundMessage(name, parameters)).Build();

}