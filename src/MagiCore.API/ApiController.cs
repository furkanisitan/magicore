using MagiCore.Results;
using Microsoft.AspNetCore.Mvc;

namespace MagiCore.API;

/// <summary>
/// A base class for an API controller.
/// </summary>
public abstract class ApiController : ControllerBase
{
    /// <inheritdoc cref="ControllerBase.Ok(object)" />
    [NonAction]
    public IActionResult ApiOk(object? value) =>
        value is null ? Ok() : Ok(ApiResultHelper.Ok(value));

    /// <inheritdoc cref="ControllerBase.Created(string, object)" />
    [NonAction]
    public IActionResult ApiCreated(string uri, object? value) =>
        Created(uri, ApiResultHelper.Created(value));

    /// <inheritdoc cref="ControllerBase.CreatedAtAction(string, object)" />
    [NonAction]
    public IActionResult ApiCreatedAtAction(string actionName, object? value) =>
        CreatedAtAction(actionName, ApiResultHelper.Created(value));

    /// <inheritdoc cref="ControllerBase.CreatedAtAction(string, object, object)" />
    [NonAction]
    public IActionResult ApiCreatedAtAction(string actionName, object routeValues, object? value) =>
        CreatedAtAction(actionName, routeValues, ApiResultHelper.Created(value));

    /// <inheritdoc cref="ControllerBase.CreatedAtAction(string, string, object, object)" />
    [NonAction]
    public IActionResult ApiCreatedAtAction(string actionName, string controllerName, object routeValues, object? value) =>
        CreatedAtAction(actionName, controllerName, routeValues, ApiResultHelper.Created(value));

    /// <inheritdoc cref="ControllerBase.NotFound(object)" />
    [NonAction]
    public IActionResult ApiNotFound(string name, params KeyValuePair<string, object>[] parameters) =>
        NotFound(ApiResultHelper.NotFound(name, parameters));

    /// <inheritdoc cref="ControllerBase.BadRequest(object)" />
    [NonAction]
    public IActionResult ApiBadRequest(object? value, string? message = null) =>
        value is null ? BadRequest(ApiResultHelper.BadRequest(message)) : BadRequest(ApiResultHelper.BadRequest(value, message));
}