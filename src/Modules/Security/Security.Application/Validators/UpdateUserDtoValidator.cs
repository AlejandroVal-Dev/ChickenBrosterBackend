using FluentValidation;
using Security.Application.DTOs.User;

namespace Security.Application.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator() 
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);

            RuleFor(x => x.NewUsername)
                .NotEmpty()
                .MinimumLength(4);

            RuleFor(x => x.NewRoleId)
                .GreaterThan(0);
        }
    }
}
