using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.Tests;
using Core.Interfaces;
using Core.QueryFilters;

namespace Core.Services
{
    public class StatsService : IStatsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<int> GetMonthlyUseOfTheAppByUser(StatsQueryFilterUseOfTheApp filter)
        {
            IQueryable<TestEntity> tests = _unitOfWork.TestRepository.GetAllAsQueryable();
            DateTime from = new DateTime(filter.Year, filter.Month, 1);
            DateTime to = from.AddMonths(1);

            IList<int> days = tests
                .Where(t => t.UserId == filter.UserId)
                .Where(t => t.CreatedOn >= from)
                .Where(t => t.CreatedOn < to)
                .Select(t => t.CreatedOn.Day)
                .Distinct()
                .ToList();

            return days;
        }
    }
}
