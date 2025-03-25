using FluentValidation;
using UserIdentityAccess.Application.DTOs;

namespace UserIdentityAccess.Application.Validators;

public class PermissionValidator : AbstractValidator<PermissionDto>
{
    public PermissionValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("Name is required");
    }
}