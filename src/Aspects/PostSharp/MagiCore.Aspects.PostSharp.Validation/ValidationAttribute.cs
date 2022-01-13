using MagiCore.Extensions;
using MagiCore.Validation;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using PostSharp.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace MagiCore.Aspects.PostSharp.Validation;

/// <summary>
/// Custom attribute that, when applied to a method, validates the first method parameter whose type matches the <see cref="_validatorType"/>'s generic type.
/// </summary>
[PSerializable]
public sealed class ValidationAttribute : OnMethodBoundaryAspect
{
    private Type _validatorType;

    /// <param name="validatorType">The type of validation class.</param>
    /// <exception cref="ValidationException">Throws when validation is failed.</exception>
    public ValidationAttribute(Type validatorType)
    {
        _validatorType = validatorType;
    }

    public override bool CompileTimeValidate(MethodBase method)
    {
        if (!_validatorType.IsAssignableToGenericType(typeof(IValidator<>)))
            throw new InvalidAnnotationException($"The validatorType is not declared in a type derived from {typeof(IValidator<>).FullName}.");

        return base.CompileTimeValidate(method);
    }

    public override void OnEntry(MethodExecutionArgs args)
    {
        var instanceType = _validatorType.GetInterface(typeof(IValidator<>).Name)?.GetGenericArguments().FirstOrDefault();
        var instances = args.Arguments.Where(x => x.GetType() == instanceType);

        var validatorConstructor = _validatorType.GetConstructor(Type.EmptyTypes);
        var validatorObject = validatorConstructor?.Invoke(Array.Empty<object>());
        var validatorMethod = _validatorType.GetMethod(nameof(IValidator<object>.Validate));

        try
        {
            foreach (var instance in instances)
                validatorMethod?.Invoke(validatorObject, new[] { instance });
        }
        catch (TargetInvocationException e)
        {
            ExceptionDispatchInfo.Capture(e.InnerException ?? e).Throw();
        }
    }
}