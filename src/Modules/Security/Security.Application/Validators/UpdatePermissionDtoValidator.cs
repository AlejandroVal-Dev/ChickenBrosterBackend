using FluentValidation;
using Security.Application.DTOs.Permission;

namespace Security.Application.Validators
{
    public class UpdatePermissionDtoValidator : AbstractValidator<UpdatePermissionDto>
    {
        public UpdatePermissionDtoValidator() 
        {
            RuleFor(x => x.PermissionId)
                .GreaterThan(0);

            RuleFor(x => x.Description)
                .MaximumLength(150)
                .When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
