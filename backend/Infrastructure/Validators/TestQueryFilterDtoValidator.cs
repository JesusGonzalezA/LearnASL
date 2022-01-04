using Core.Contracts.Incoming;
using Core.Options;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Infrastructure.Validators
{
    public class TestQueryFilterDtoValidator : AbstractValidator<TestQueryFilterDto>
    {
        private readonly PaginationOptions _paginationOptions;

        public TestQueryFilterDtoValidator
        (
            IOptions<PaginationOptions> paginationOptions
        )
        {
            _paginationOptions = paginationOptions.Value;

            RuleFor(filter => filter.PageNumber)
                .GreaterThan(0);

            RuleFor(filter => filter.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(_paginationOptions.MaximumPageSize);

            RuleFor(filter => filter.Difficulty)
                .IsInEnum()
                .When(filter => filter.Difficulty.HasValue);

            RuleFor(filter => filter.TestType)
                .IsInEnum()
                .When(filter => filter.TestType.HasValue);

            When(filter => filter.FromDate.HasValue && filter.ToDate.HasValue, () =>
            {
                RuleFor(filter => filter)
                    .Must(HasCorrectInterval)
                    .WithMessage("The interval is not correct.");
            });
        }

        private bool HasCorrectInterval(TestQueryFilterDto filter)
        {
            return filter.ToDate >= filter.FromDate;
        }
    }
}
