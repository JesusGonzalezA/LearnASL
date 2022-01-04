using FluentValidation;
using Core.Contracts.Incoming;

namespace Infrastructure.Validators
{
    public class ChangeEmailDtoValidator : AbstractValidator<ChangeEmailDto>
    {
        public ChangeEmailDtoValidator()
        {
            RuleFor(dto => dto.Email)
                .NotNull()
                .EmailAddress();
        }
    }
}
