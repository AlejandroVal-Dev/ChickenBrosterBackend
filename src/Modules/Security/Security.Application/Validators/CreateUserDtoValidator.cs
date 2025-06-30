using FluentValidation;
using Security.Application.DTOs.User;

namespace Security.Application.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator() 
        {
            // User
            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(30);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.RoleId)
                .GreaterThan(0);

            // Person
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.LastName1)
                .NotEmpty();

            RuleFor(x => x.DocumentId)
                .NotEmpty();

            RuleFor(x => x.DocumentType)
                .IsInEnum();

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email));
        }
    }
}
