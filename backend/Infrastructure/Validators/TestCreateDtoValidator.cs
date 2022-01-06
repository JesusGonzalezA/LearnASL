using Core.Contracts.Incoming;
using FluentValidation;

namespace Infrastructure.Validators
{
    public class TestCreateDtoValidator : AbstractValidator<TestCreateDto>
    {
        public TestCreateDtoValidator()
        {
            RuleFor(test => test.NumberOfQuestions)
                .GreaterThan(0)
                .LessThanOrEqualTo(10)
                .NotNull();

            RuleFor(test => test.Difficulty)
                .NotNull();
            RuleFor(test => test.Difficulty)
                .IsInEnum();

            RuleFor(test => test.TestType)
                .NotNull();
            RuleFor(test => test.TestType)
                .IsInEnum();
        }
    }
}
