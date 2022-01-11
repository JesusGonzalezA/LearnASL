using Core.Contracts.Incoming;
using FluentValidation;

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
