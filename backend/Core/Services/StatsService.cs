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
    public partial class StatsService : IStatsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITestService _testService;

        public StatsService(IUnitOfWork unitOfWork, ITestService testService)
        {
            _unitOfWork = unitOfWork;
            _testService = testService;
        }

        public int GetBestStreak(Guid userId)
        {
            IQueryable<TestEntity> tests = _unitOfWork.TestRepository
                .GetAllAsQueryable()
                .Where(test => test.UserId == userId);
            IEnumerable<DateTime> dates = tests
                .Select(test => test.CreatedOn.Date)
                .Distinct();

            if (!dates.Any())
                return 0;

            List<int> groupOfStreaks = dates
                .GroupWhile((date1, date2) => (date1.AddDays(1) == date2))
                .Select(x => x.Count())
                .ToList();

            return groupOfStreaks.Max();
        }

        public int GetCurrentStreak(Guid userId)
        {
            IQueryable<TestEntity> tests = _unitOfWork.TestRepository
                .GetAllAsQueryable()
                .Where(test => test.UserId == userId);
            IEnumerable<DateTime> dates = tests
                .Select(test => test.CreatedOn.Date)
                .Distinct();

            if (!dates.Any())
                return 0;

            IEnumerable<DateTime> lastStreak = dates
                .GroupWhile((date1, date2) => (date1.AddDays(1) == date2))
                .Last()
                .ToList();

            DateTime lastDateOfLastStreak = lastStreak.Last().Date;

            return (lastDateOfLastStreak.AddDays(1) >= DateTime.Today)
                ? lastStreak.Count()
                : 0;                    // The streak has passed
        }

        public IEnumerable<int> GetMonthlyUseOfTheAppByUser(StatsQueryFilterUseOfTheApp filter)
        {
            IQueryable<TestEntity> tests = _unitOfWork.TestRepository
                .GetAllAsQueryable()
                .Where(t => t.UserId == filter.UserId);
            DateTime from = new DateTime(filter.Year, filter.Month, 1);
            DateTime to = from.AddMonths(1);

            IList<int> days = tests
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
                Tuple<DateTime, DateTime> interval = CalculateInterval(filter);

                learntWords = learntWords
                    .Where(l => l.CreatedOn >= interval.Item1)
                    .Where(l => l.CreatedOn < interval.Item2);
            }

            return learntWords.Count();
        }

        public async Task<double> GetPercentOfWordsLearntByUser(Guid userId)
        {
            int sizeOfDataset = await _unitOfWork.DatasetRepository.GetSizeOfDataset();
            int numberOfWordsLearntByUser = GetNumberOfWordsLearntByUser(userId);

            return (double)numberOfWordsLearntByUser / sizeOfDataset;
        }

        public double GetSuccessRate(StatsQueryFilterSuccessRate filter)
        {
            Tuple<DateTime, DateTime> interval = CalculateInterval(filter);

            TestQueryFilter testFilter = new TestQueryFilter()
            {
                UserId = filter.UserId,
                Difficulty = filter.Difficulty,
                FromDate = interval.Item1,
                ToDate = interval.Item2,
            };
            IList<TestWithQuestions> tests = _testService.GetAllTests(testFilter);

            int totalQuestions = 0;
            int totalRightQuestions = 0;
            foreach(TestWithQuestions test in tests)
            {
                totalQuestions += test.Questions.Count();

                foreach(BaseQuestionEntity question in test.Questions)
                {
                    if(question.IsQuestionCorrect())
                    {
                        totalRightQuestions++;
                    }
                }
            }

            if (totalQuestions == 0) return 0;

            return (double)totalRightQuestions / totalQuestions;
        }
    }

    public partial class StatsService
    {
        private Tuple<DateTime, DateTime> CalculateInterval(StatsQueryFilterSuccessRate filter)
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

            return new Tuple<DateTime, DateTime>(from, to);
        }

        private Tuple<DateTime, DateTime> CalculateInterval(StatsQueryFilterNumberOfLearntWords filter)
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

            return new Tuple<DateTime, DateTime>(from, to);
        }
    }
}
