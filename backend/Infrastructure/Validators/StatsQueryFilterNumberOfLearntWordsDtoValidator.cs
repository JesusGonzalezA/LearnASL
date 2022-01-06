using System;
using Core.Contracts.Incoming;
using FluentValidation;

namespace Infrastructure.Validators
{
    public class StatsQueryFilterNumberOfLearntWordsDtoValidator : AbstractValidator<StatsQueryFilterNumberOfLearntWordsDto>
    {
        public StatsQueryFilterNumberOfLearntWordsDtoValidator()
        {
            RuleFor(filter => filter.Year)
                .LessThanOrEqualTo(DateTime.Now.Year);

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
        }

        private bool HasCorrectValue(StatsQueryFilterNumberOfLearntWordsDto filter)
        {
            try
            {
                DateTime date = new DateTime(filter.Year, filter.Month ?? 1, filter.Day ?? 1);
                return true;
            }
            catch(ArgumentOutOfRangeException)
            {
                return false;
            }
        }
    }
}
