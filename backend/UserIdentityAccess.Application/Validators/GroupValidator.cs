using FluentValidation;
using UserIdentityAccess.Application.DTOs;

namespace UserIdentityAccess.Application.Validators;

public class GroupValidator : AbstractValidator<GroupDto>
{
    public GroupValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("Name is required");
    }
}