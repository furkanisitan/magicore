using FluentValidation;
using System.Linq.Expressions;

namespace MagiCore.Validation.FluentValidation;

/// <summary>
/// Extension methods that provide the fluent validators.
/// </summary>
public static class ValidatorExtensions
{
    /// <summary>
    /// Defines a unique validator on the current rule builder.
    /// Validation will fail if the property is not unique.
    /// </summary>
    /// <typeparam name="T">Type of object being validated.</typeparam>
    /// <typeparam name="TProperty">Type of property being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="originalItem">A function that returns the original instance of the object being validated.</param>
    /// <param name="primaryKeys">Lambda expressions that return the primary key properties of the object being validated.</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, TProperty> Unique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Func<T, object> originalItem, params Expression<Func<T, object>>[] primaryKeys) =>
        ruleBuilder.SetValidator(new UniqueValidator<T, TProperty>(originalItem, primaryKeys));
}