namespace MagiCore.Exceptions;

/// <summary>
/// An exception representing a mismatch between the route parameter and the body property.
/// </summary>
public class RouteBodyMismatchException : Exception
{
    private const string DefaultMessage = "The value in the route and the value in the body do not match.";

    public string? RouteName { get; }
    public string? PropertyName { get; }
    public IEnumerable<string> Errors { get; }

    public RouteBodyMismatchException(string? routeName, string? propertyName)
        : this(DefaultMessage, routeName, propertyName)
    {
    }

    public RouteBodyMismatchException(string? message, string? routeName, string? propertyName)
        : this(message, null, routeName, propertyName)
    {
    }

    /// <summary>
    /// The constructor of <see cref="RouteBodyMismatchException"/>.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    /// <param name="routeName">The name of the value in the route.</param>
    /// <param name="propertyName">The name of the value in the body.</param>
    public RouteBodyMismatchException(string? message, Exception? innerException, string? routeName, string? propertyName)
        : base(message, innerException)
    {
        RouteName = routeName;
        PropertyName = propertyName;
        Errors = Enumerable.Repeat(BuildRouteBodyMismatchError(routeName, propertyName), 1);
    }

    private static string BuildRouteBodyMismatchError(string? routeName, string? propertyName)
    {
        return $"{propertyName}: it doesn't match {routeName} in the route.";
    }

}