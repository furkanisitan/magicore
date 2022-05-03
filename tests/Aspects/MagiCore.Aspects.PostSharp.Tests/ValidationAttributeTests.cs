using MagiCore.Exceptions;
using MagiCore.Validation;
using NUnit.Framework;
using System;

namespace MagiCore.Aspects.PostSharp.Tests;

[TestFixture]
internal class ValidationAttributeTests
{
    [Test]
    public void InstanceMethod_NameIsValid_DoesNotThrowsException()
    {
        Assert.DoesNotThrow(() => InstanceMethod(new Product { Name = "Laptop" }));
    }

    [Test]
    public void InstanceMethod_NameIsEmpty_ThrowsValidationException()
    {
        Assert.Throws<ValidationException>(() => InstanceMethod(new Product { Name = "" }));
    }

    [Validation(typeof(ProductValidator))]
    private static void InstanceMethod(Product? product)
    {
        Console.WriteLine(product is null);
    }

    protected class Product
    {
        public string? Name { get; set; }
    }

    protected class ProductValidator : IValidator<Product>
    {
        public void Validate(Product instance)
        {
            if (string.IsNullOrWhiteSpace(instance.Name))
                throw new ValidationException("Name must not be null or whitespace.");
        }
    }
}