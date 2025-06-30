using FluentValidation;
using Security.Application.DTOs.Permission;

namespace Security.Application.Validators
{
    public class CreatePermissionDtoValidator : AbstractValidator<CreatePermissionDto>
    {
        public CreatePermissionDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty();

            RuleFor(x => x.Description)
                .MaximumLength(150)
                .When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
