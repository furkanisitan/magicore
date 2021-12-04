using FluentValidation.Internal;
using FluentValidation.Validators;
using System;
using System.Linq.Expressions;

namespace FluentValidation.CustomPropertyValidators;

public class UniqueValidator<T, TProperty> : PropertyValidator<T, TProperty>, IUniqueValidator
{

    private readonly Func<T, object> _originalItem;
    private readonly Expression<Func<T, object>>[] _primaryKeys;

    /// <param name="originalItem">A function that returns the original instance of the object being validated.</param>
    /// <param name="primaryKeys">Lambda expressions that return the primary key properties of the object being validated.</param>
    public UniqueValidator(Func<T, object> originalItem, params Expression<Func<T, object>>[] primaryKeys)
    {
        _originalItem = originalItem;
        _primaryKeys = primaryKeys;
    }

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        var originalItemInstance = _originalItem(context.InstanceToValidate);
        if (originalItemInstance == null) return true;

        var hasEqualPrimaryKeys = false;

        foreach (var primaryKey in _primaryKeys)
        {
            var propertyName = primaryKey.GetMember().Name;
            var propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo != null)
            {
                var itemPrimaryKeyValue = propertyInfo.GetValue(context.InstanceToValidate);
                var originalItemPrimaryKeyValue = propertyInfo.GetValue(originalItemInstance);

                if (itemPrimaryKeyValue != null && !itemPrimaryKeyValue.Equals(originalItemPrimaryKeyValue)) return false;
                hasEqualPrimaryKeys = true;
            }

        }

        return hasEqualPrimaryKeys;
    }

    public override string Name => "UniqueValidator";

    protected override string GetDefaultMessageTemplate(string errorCode) =>
        "{PropertyName} must be unique.";

}

public interface IUniqueValidator : IPropertyValidator
{
}