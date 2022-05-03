using FluentValidation;
using FluentValidation.Validators;

namespace MagiCore.Validation.FluentValidation;

/// <summary>
/// A property validator to check if the property exists in the specified values.
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
/// <typeparam name="TProperty">The type of property being validated.</typeparam>
public class ExistsValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    private readonly TProperty[] _values;

    /// <param name="values">The array of values to check.</param>
    public ExistsValidator(params TProperty[] values)
    {
        _values = values;
    }

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        context.MessageFormatter.AppendArgument("Values", string.Join(',', _values));
        return _values.Contains(value);
    }

    public override string Name => "ExistsValidator";

    protected override string GetDefaultMessageTemplate(string errorCode) =>
        "The '{PropertyName}' must be one of the values '{Values}'.";
}