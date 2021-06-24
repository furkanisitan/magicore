using FluentValidation.Validators;
using System;

namespace FluentValidation.CustomPropertyValidators
{
    public class UniqueValidator<T, TProperty> : PropertyValidator<T, TProperty>
    {
        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            // TODO Implement this method
            throw new NotImplementedException();
        }

        public override string Name => "UniqueValidator";
    }
}
