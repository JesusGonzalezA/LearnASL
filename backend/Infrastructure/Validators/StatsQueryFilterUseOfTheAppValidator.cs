using System;
using Core.Contracts.Incoming;
using FluentValidation;

namespace Infrastructure.Validators
{
    public class StatsQueryFilterUseOfTheAppValidator : AbstractValidator<StatsQueryFilterUseOfTheAppDto>
    {
        public StatsQueryFilterUseOfTheAppValidator()
        {
            RuleFor(filter => filter.Year)
                .LessThanOrEqualTo(DateTime.Now.Year);

            RuleFor(filter => filter.Month)
                .InclusiveBetween(1, 12);
        }
    }
}
