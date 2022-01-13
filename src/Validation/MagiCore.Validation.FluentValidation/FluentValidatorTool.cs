using FluentValidation;

namespace MagiCore.Validation.FluentValidation;

public class FluentValidatorTool
{
    /// <summary>
    /// Validates the <paramref name="instance"/>.
    /// </summary>
    /// <typeparam name="T">The type of <paramref name="instance"/>.</typeparam>
    /// <param name="validator">The validator to validate the <paramref name="instance"/>.</param>
    /// <param name="instance">The instance to validate.</param>
    /// <exception cref="Core.Exceptions.ValidationException">Throws when <paramref name="instance"/> not validate.</exception>
    public static void Validate<T>(AbstractValidator<T> validator, T instance)
    {
        var results = validator.Validate(instance);
        if (results is { IsValid: false })
            throw new MagiCore.Exceptions.ValidationException(results.Errors.Select(x => x.ErrorMessage));
    }
}