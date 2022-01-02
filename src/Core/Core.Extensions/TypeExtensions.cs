namespace Core.Extensions;

public static class TypeExtensions
{
    #region IsAssignableToGenericType

    /// <summary>
    /// Determines whether the current type can be assigned to a variable of the specified <paramref name="targetGenericType"/>.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="targetGenericType">The generic type to compare with the current type.</param>
    /// <returns><see langword="true"/> if the current type can be assigned to the <paramref name="targetGenericType"/>, otherwise <see langword="false"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws when <paramref name="type"/> or <paramref name="targetGenericType"/> is <see langword="null"/>.</exception>

    public static bool IsAssignableToGenericType(this Type type, Type targetGenericType)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));
        if (targetGenericType == null) throw new ArgumentNullException(nameof(targetGenericType));

        return type == targetGenericType || type.MapsToGenericTypeDefinition(targetGenericType) || type.HasInterfaceThatMapsToGenericTypeDefinition(targetGenericType) || type.BaseType.IsAssignableToGenericType(targetGenericType);
    }

    private static bool HasInterfaceThatMapsToGenericTypeDefinition(this Type type, Type targetGenericType) =>
        type.GetInterfaces().Where(it => it.IsGenericType).Any(it => it.GetGenericTypeDefinition() == targetGenericType);

    private static bool MapsToGenericTypeDefinition(this Type type, Type targetGenericType) =>
        targetGenericType.IsGenericTypeDefinition && type.IsGenericType && type.GetGenericTypeDefinition() == targetGenericType;

    #endregion
}