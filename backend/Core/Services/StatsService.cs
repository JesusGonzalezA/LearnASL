using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Tests;
using Core.Extensions;
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

        public int GetBestStreak(Guid userId)
        {
            IQueryable<TestEntity> tests = _unitOfWork.TestRepository.GetAllAsQueryable();
            IEnumerable<DateTime> dates = tests.
                Select(test => test.CreatedOn.Date)
                .Distinct();

            List<int> groupOfStreaks = dates
                .GroupWhile((date1, date2) => (date1.AddDays(1) == date2))
                .Select(x => x.Count())
                .ToList();

            if (groupOfStreaks == null)
                return 0;

            return groupOfStreaks.Max();
        }

        public int GetCurrentStreak(Guid userId)
        {
            IQueryable<TestEntity> tests = _unitOfWork.TestRepository.GetAllAsQueryable();
            IEnumerable<DateTime> dates = tests.
                Select(test => test.CreatedOn.Date)
                .Distinct();

            IEnumerable<DateTime> lastStreak = dates
                .GroupWhile((date1, date2) => (date1.AddDays(1) == date2))
                .Last()
                .ToList();

            if (!lastStreak.Contains(DateTime.Now.Date))
                return 0;

            return lastStreak.Count();
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

        public int GetNumberOfWordsLearntByUser(Guid userId, StatsQueryFilterNumberOfLearntWords filter = null)
        {
            IQueryable<LearntWordEntity> learntWords = _unitOfWork
                    .LearntWordRepository
                    .GetAllAsQueryable()
                    .Where(l => l.UserId == userId);

            if (filter != null)
            {
                DateTime from = new DateTime(filter.Year, filter.Month ?? 1, filter.Day ?? 1);
                DateTime to;

                if (filter.Day.HasValue)
                {
                    to = from.AddDays(1);
                }
                else if (filter.Month.HasValue)
                {
                    to = from.AddMonths(1);
                }
                else
                {
                    to = from.AddYears(1);
                }

                learntWords = learntWords
                    .Where(l => l.CreatedOn >= from)
                    .Where(l => l.CreatedOn < to);
            }

            return learntWords.Count();
        }

        public async Task<double> GetPercentOfWordsLearntByUser(Guid userId)
        {
            int sizeOfDataset = await _unitOfWork.DatasetRepository.GetSizeOfDataset();
            int numberOfWordsLearntByUser = GetNumberOfWordsLearntByUser(userId);

            return (double)numberOfWordsLearntByUser / sizeOfDataset;
        }

        public async Task<double> GetSuccessRate(Guid userId)
        {
            int sizeOfDataset = await _unitOfWork.DatasetRepository.GetSizeOfDataset();
            int numberOfWordsLearntByUser = GetNumberOfWordsLearntByUser(userId);

            return (double)numberOfWordsLearntByUser / sizeOfDataset;
        }
    }
}
