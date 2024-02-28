using FluentValidation;


namespace Hrm.Application.DTOs.Role.Validators
{
    public class UpdateRoleDtoValidator : AbstractValidator<RoleDto>
    {
        public UpdateRoleDtoValidator()
        {
            Include(new IRoleDtoValidator());

            RuleFor(p => p.RoleId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}