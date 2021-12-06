using FluentValidation;
using Core.Contracts.Incoming;
using System.Text.RegularExpressions;

namespace Infraestructure.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(login => login.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(login => login.Password)
                .NotNull()
                .Length(6, 15);

            RuleFor(login => login.Password)
                .Must(HasValidPassword)
                .WithMessage("'{PropertyName}' should have at least one digit, one symbol, a lowercase letter and an uppercase letter.");
        }

        private bool HasValidPassword(string pw)
        {
            var lowercase = new Regex("[a-z]+");
            var uppercase = new Regex("[A-Z]+");
            var digit = new Regex("(\\d)+");
            var symbol = new Regex("(\\W)+");

            return (lowercase.IsMatch(pw) && uppercase.IsMatch(pw) && digit.IsMatch(pw) && symbol.IsMatch(pw));
        }
    }
}
