using System;
using Core.Contracts.Incoming;
using FluentValidation;

namespace Infrastructure.Validators
{
    public class StatsQueryFilterSuccessRateDtoValidator : AbstractValidator<StatsQueryFilterSuccessRateDto>
    {
        public StatsQueryFilterSuccessRateDtoValidator()
        {
            RuleFor(filter => filter.Month)
                .NotNull()
                .When(filter => filter.Day.HasValue);

            RuleFor(filter => filter.Month)
                .InclusiveBetween(1, 12)
                .When(filter => filter.Month.HasValue);

            When(filter => filter.Day.HasValue, () =>
            {
                RuleFor(filter => filter)
                    .Must(HasCorrectValue)
                    .WithMessage("The date is incorrect.");
            });

            RuleFor(filter => filter.Difficulty)
                .IsInEnum()
                .When(filter => filter.Difficulty.HasValue);
        }

        private bool HasCorrectValue(StatsQueryFilterSuccessRateDto filter)
        {
            try
            {
                DateTime date = new DateTime(filter.Year, filter.Month ?? 1, filter.Day ?? 1);
                return date <= DateTime.Now;
            }
            catch(ArgumentOutOfRangeException)
            {
                return false;
            }
        }
    }
}
