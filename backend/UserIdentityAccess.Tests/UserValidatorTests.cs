using FluentValidation.TestHelper;
using UserIdentityAccess.Application.DTOs;
using UserIdentityAccess.Application.Validators;
using Xunit;

namespace UserIdentityAccess.Tests;
public class UserValidatorTests
{
    private readonly UserValidator _validator;

    public UserValidatorTests()
    {
        _validator = new UserValidator();
    }

    [Fact]
    public void Should_Have_Error_When_First_Name_Is_Empty()
    {
        var model = new UserDto { FirstName = "", LastName = "steve", Email = "test@example.com" };
        var result = _validator.TestValidate(model);
        
        result.ShouldHaveValidationErrorFor(user => user.FirstName)
            .WithErrorMessage("FirstName is required");
    }
    
    [Fact]
    public void Should_Have_Error_When_Last_Name_Is_Empty()
    {
        var model = new UserDto { FirstName = "Alex", LastName = "", Email = "test@example.com" };
        var result = _validator.TestValidate(model);
        
        result.ShouldHaveValidationErrorFor(user => user.LastName)
            .WithErrorMessage("LastName is required");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var model = new UserDto { FirstName = "Alex", LastName = "ELc",Email = "invalid-email" };
        var result = _validator.TestValidate(model);
        
        result.ShouldHaveValidationErrorFor(user => user.Email)
            .WithErrorMessage("Invalid email format");
    }

    [Fact]
    public void Should_Pass_Validation_When_Data_Is_Valid()
    {
        var model = new UserDto { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
        var result = _validator.TestValidate(model);
        
        result.ShouldNotHaveValidationErrorFor(user => user.FirstName);
        result.ShouldNotHaveValidationErrorFor(user => user.LastName);
        result.ShouldNotHaveValidationErrorFor(user => user.Email);
    }
}
