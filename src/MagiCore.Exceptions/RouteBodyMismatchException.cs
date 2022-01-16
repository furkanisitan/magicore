namespace MagiCore.Exceptions;

/// <summary>
/// An exception representing a mismatch between the route parameter and the body property.
/// </summary>
public class RouteBodyMismatchException : Exception
{
    /// <summary>
    /// Creates a new RouteBodyMismatchException.
    /// </summary>
    /// <param name="routeName">The name of the value in the route.</param>
    public RouteBodyMismatchException(string routeName) : base($"The {routeName} in the route does not match the {routeName} in the body.")
    {
    }

    /// <summary>
    /// Creates a new RouteBodyMismatchException.
    /// </summary>
    /// <param name="routeName">The name of the value in the route.</param>
    /// <param name="propertyName">The name of the value in the body.</param>
    public RouteBodyMismatchException(string routeName, string propertyName) : base($"The {routeName} in the route does not match the {propertyName} in the body.")
    {
    }
}