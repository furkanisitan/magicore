using FluentValidation;
using System;

namespace MagiCore.Validation.FluentValidation.Tests;

internal class TestValidator : InlineValidator<TestUser>
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