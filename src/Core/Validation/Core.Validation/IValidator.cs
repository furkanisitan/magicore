using Core.Exceptions;

namespace Core.Validation;

/// <summary>
/// Defines a validator for a particular type.
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
public interface IValidator<in T>
{
    /// <summary>
    /// Validates the specified instance.
    /// </summary>
    /// <param name="instance">The object to validate</param>
    /// <exception cref="ArgumentNullException">Throws when <paramref name="instance"/> is <see langword="null"/>.</exception>
    /// <exception cref="ValidationException">Throws when <paramref name="instance"/> not validate.</exception>
    void Validate(T instance);
}