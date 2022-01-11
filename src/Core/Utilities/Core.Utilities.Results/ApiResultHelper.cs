using Core.Utilities.Messaging;

namespace Core.Utilities.Results;

public static class ApiResultHelper
{
    public static IResult Ok() =>
        Result.Builder().Success().Message(ApiResultMessages.Ok).Build();

    public static IResult<T> Ok<T>(T payload) =>
        Result.Builder(payload).Success().Message(ApiResultMessages.Ok).Build();

    public static IResult<T> Created<T>(T payload) =>
        Result.Builder(payload).Success().Message(ApiResultMessages.Created).Build();

    public static IResult BadRequest<T>(string? message = null) =>
        Result.Builder().Success().Message(message ?? ApiResultMessages.ErrBadRequest).Build();

    public static IResult<T> BadRequest<T>(T payload, string? message = null) =>
        Result.Builder(payload).Success().Message(message ?? ApiResultMessages.ErrBadRequest).Build();

    public static IResult NotFound(string? name, params KeyValuePair<string, object>[] parameters) =>
        Result.Builder().Message(ApiResultMessages.ErrNotFound).AddError(MessageHelper.BuildNotFoundMessage(name, parameters)).Build();

}