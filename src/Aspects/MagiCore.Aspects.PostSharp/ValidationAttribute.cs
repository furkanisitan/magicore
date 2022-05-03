using MagiCore.Extensions;
using MagiCore.Validation;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using PostSharp.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace MagiCore.Aspects.PostSharp;

/// <summary>
/// Aspect that, when applied to a method,
/// validates all parameters whose type matches the validator's generic type.
/// </summary>
[PSerializable]
public sealed class ValidationAttribute : OnMethodBoundaryAspect
{
    private Type _validatorType;

    /// <summary>
    /// The constructor of <see cref="ValidationAttribute"/>.
    /// <remarks>
    /// The validator class must have a parameterless constructor and implement <see cref="IValidator{T}"/>.
    /// These requirements are checked at compile time and an <see cref="InvalidAnnotationException"/> is thrown if the requirements are not met.
    /// </remarks>
    /// </summary>
    /// <param name="validatorType">The type of validation class.</param>
    /// <exception cref="ValidationException">Throws when validation is failed.</exception>
    public ValidationAttribute(Type validatorType)
    {
        _validatorType = validatorType;
    }

    public override bool CompileTimeValidate(MethodBase method)
    {
        if (!_validatorType.IsAssignableToGenericType(typeof(IValidator<>)))
            throw new InvalidAnnotationException($"The {_validatorType} is not declared in a type derived from {typeof(IValidator<>).FullName}.");

        if (_validatorType.GetConstructor(Type.EmptyTypes) is null)
            throw new InvalidAnnotationException($"The {_validatorType} must have a parameterless constructor.");

        return base.CompileTimeValidate(method);
    }

    public override void OnEntry(MethodExecutionArgs args)
    {
        var instanceType = _validatorType.GetInterface(typeof(IValidator<>).Name)!.GetGenericArguments()[0];
        var instances = args.Arguments.Where(x => x.GetType() == instanceType);
        
        var validatorObject = _validatorType.GetConstructor(Type.EmptyTypes)!.Invoke(Array.Empty<object>());
        var validatorMethod = _validatorType.GetMethod(nameof(IValidator<object>.Validate));

        try
        {
            foreach (var instance in instances)
                validatorMethod!.Invoke(validatorObject, new[] { instance });
        }
        catch (TargetInvocationException e)
        {
            ExceptionDispatchInfo.Capture(e.InnerException ?? e).Throw();
        }
    }
}