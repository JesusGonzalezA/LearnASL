using Core.Contracts.Incoming;
using FluentValidation;

namespace Infraestructure.Validators
{
    public class TestQueryFilterDtoValidator : AbstractValidator<TestQueryFilterDto>
    {
        public TestQueryFilterDtoValidator()
        {
            RuleFor(filter => filter.PageNumber)
                .GreaterThan(0);

            RuleFor(filter => filter.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(15);

            RuleFor(filter => filter.Difficulty)
                .IsInEnum()
                .When(filter => filter.Difficulty.HasValue);

            RuleFor(filter => filter.TestType)
                .IsInEnum()
                .When(filter => filter.TestType.HasValue);
        }
    }
}
