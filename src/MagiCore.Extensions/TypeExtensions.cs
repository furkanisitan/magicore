namespace MagiCore.Extensions;

/// <summary>
/// This class contains extension methods for the <see cref="Type"/>.
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// Determines whether the current type can be assigned to a variable of the specified <paramref name="genericType"/>.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="genericType">The generic type to compare with the current type.</param>
    /// <returns><see langword="true"/> if the current type can be assigned to the <paramref name="genericType"/>, otherwise <see langword="false"/>.</returns>
    public static bool IsAssignableToGenericType(this Type type, Type? genericType)
    {
        if (genericType is null)
            return false;

        return type.IsAssignableTo(genericType) ||
               type.IsGenericType && type.GetGenericTypeDefinition() == genericType ||
               type.GetInterfaces().Where(x => x.IsGenericType).Any(x => x.GetGenericTypeDefinition() == genericType) ||
               type.BaseType is not null && type.BaseType.IsAssignableToGenericType(genericType);
    }

}
