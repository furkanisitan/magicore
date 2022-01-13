using MagiCore.Messaging;

namespace MagiCore.Results;

/// <summary>
/// Contains helper methods that generate responses for APIs.
/// </summary>
public static class ApiResultHelper
{
    /// <summary>
    /// Creates a <see cref="IResult"/> object that produces an empty Ok response.
    /// </summary>
    /// <returns>A <see cref="IResult"/> instance.</returns>
    public static IResult Ok() =>
        Result.Builder().Success().Message(ApiResultMessages.Ok).Build();

    /// <summary>
    /// Creates a <see cref="IResult{T}"/> object that produces an Ok response with payload.
    /// </summary>
    /// <typeparam name="T">The type of <paramref name="payload"/>.</typeparam>
    /// <param name="payload">The data the response will contain.</param>
    /// <returns>A <see cref="IResult{T}"/> instance.</returns>
    public static IResult<T> Ok<T>(T payload) =>
        Result.Builder(payload).Success().Message(ApiResultMessages.Ok).Build();

    /// <summary>
    /// Creates a <see cref="IResult{T}"/> object that produces an Created response with payload.
    /// </summary>
    /// <typeparam name="T">The type of <paramref name="payload"/>.</typeparam>
    /// <param name="payload">The data the response will contain.</param>
    /// <returns>A <see cref="IResult{T}"/> instance.</returns>
    public static IResult<T> Created<T>(T payload) =>
        Result.Builder(payload).Success().Message(ApiResultMessages.Created).Build();

    /// <summary>
    /// Creates a <see cref="IResult"/> object that produces an empty BadRequest response with message.
    /// </summary>
    /// <param name="message">The message the response will contain.</param>
    /// <returns>A <see cref="IResult"/> instance.</returns>
    public static IResult BadRequest(string? message = null) =>
        Result.Builder().Success().Message(message ?? ApiResultMessages.ErrBadRequest).Build();

    /// <summary>
    /// Creates a <see cref="IResult{T}"/> object that produces an BadRequest response with payload.
    /// </summary>
    /// <typeparam name="T">The type of <paramref name="payload"/>.</typeparam>
    /// <param name="payload">The data the response will contain.</param>
    /// <param name="message">The message the response will contain.</param>
    /// <returns>A <see cref="IResult{T}"/> instance.</returns>
    public static IResult<T> BadRequest<T>(T payload, string? message = null) =>
        Result.Builder(payload).Success().Message(message ?? ApiResultMessages.ErrBadRequest).Build();

    /// <summary>
    /// Creates a <see cref="IResult"/> object that produces an NotFound response with created message.
    /// </summary>
    /// <param name="name">The name of the not found record.</param>
    /// <param name="parameters">Query parameters key-value pairs.</param>
    /// <returns>A <see cref="IResult"/> instance.</returns>
    public static IResult NotFound(string? name, params KeyValuePair<string, object>[] parameters) =>
        Result.Builder().Message(ApiResultMessages.ErrNotFound).AddError(MessageHelper.BuildNotFoundMessage(name, parameters)).Build();
}