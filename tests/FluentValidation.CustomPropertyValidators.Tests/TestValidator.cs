using System;

namespace FluentValidation.CustomPropertyValidators.Tests;

internal class TestValidator : InlineValidator<User>
{
    public new CascadeMode CascadeMode
    {
        get => base.CascadeMode;
        set => base.CascadeMode = value;
    }

    public TestValidator() { }

    public TestValidator(params Action<TestValidator>[] actions)
    {
        foreach (var action in actions)
            action(this);
    }
}