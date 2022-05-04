using MagiCore.Results;
using Microsoft.AspNetCore.Mvc;

namespace MagiCore.API;

/// <summary>
/// A base class for an API controller.
/// </summary>
public abstract class ApiController : ControllerBase
{
    private const string MessageOk = "The request has been processed successfully.";
    private const string MessageCreated = "Resource(s) added successfully.";

    /// <inheritdoc cref="ControllerBase.Ok(object)" />
    [NonAction]
    public IActionResult ApiOk(object? value) =>
        value is null ? Ok() : Ok(Result.Builder(value).Success().Message(MessageOk).Build());

    /// <inheritdoc cref="ControllerBase.Created(string, object)" />
    [NonAction]
    public IActionResult ApiCreated(string uri, object? value) =>
        Created(uri, Result.Builder(value).Success().Message(MessageCreated).Build());

    /// <inheritdoc cref="ControllerBase.CreatedAtAction(string, object)" />
    [NonAction]
    public IActionResult ApiCreatedAtAction(string actionName, object? value) =>
        CreatedAtAction(actionName, Result.Builder(value).Success().Message(MessageCreated).Build());

    /// <inheritdoc cref="ControllerBase.CreatedAtAction(string, object, object)" />
    [NonAction]
    public IActionResult ApiCreatedAtAction(string actionName, object routeValues, object? value) =>
        CreatedAtAction(actionName, routeValues, Result.Builder(value).Success().Message(MessageCreated).Build());

    /// <inheritdoc cref="ControllerBase.CreatedAtAction(string, string, object, object)" />
    [NonAction]
    public IActionResult ApiCreatedAtAction(string actionName, string controllerName, object routeValues, object? value) =>
        CreatedAtAction(actionName, controllerName, routeValues, Result.Builder(value).Success().Message(MessageCreated).Build());
}