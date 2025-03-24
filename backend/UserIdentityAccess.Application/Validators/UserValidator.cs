using FluentValidation;
using UserIdentityAccess.Application.DTOs;

namespace UserIdentityAccess.Application.Validators;

public class UserValidator : AbstractValidator<UserDto>
{
    public UserValidator()
    {
        RuleFor(user => user.FirstName)
            .NotEmpty().WithMessage("FirstName is required");
        
        RuleFor(user => user.LastName)
            .NotEmpty().WithMessage("LastName is required");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");
    }
}