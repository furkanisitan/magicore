using System;

namespace MagiCore.Validation.FluentValidation.Tests;

internal class TestUser
{
    public int Id { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }


    public double Height { get; set; }

    public bool IsEmailVerified { get; set; }

    public DateTime DateOfBirth { get; set; }
}