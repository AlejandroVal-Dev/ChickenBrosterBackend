using FluentValidation;
using Security.Application.DTOs.Role;

namespace Security.Application.Validators
{
    public class AssignPermissionDtoValidator : AbstractValidator<AssignPermissionDto>
    {
        public AssignPermissionDtoValidator()
        {
            RuleFor(x => x.RoleId)
                .GreaterThan(0);

            RuleFor(x => x.PermissionId)
                .GreaterThan(0);
        }
    }
}
