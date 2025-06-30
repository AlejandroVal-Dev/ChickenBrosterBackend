using FluentValidation;
using Security.Application.DTOs.Role;

namespace Security.Application.Validators
{
    public class CreateRoleDtoValidator : AbstractValidator<CreateRoleDto>
    {
        public CreateRoleDtoValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.Description)
                .MaximumLength(200)
                .When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
