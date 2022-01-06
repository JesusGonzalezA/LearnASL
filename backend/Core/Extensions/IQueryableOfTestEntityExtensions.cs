using System.Linq;
using Core.Entities.Tests;
using Core.QueryFilters;

namespace Core.Extensions
{
    public static class IQueryableOfTestEntityExtensions
    {
        public static IQueryable<TestEntity>
        Filter
        (
            this IQueryable<TestEntity> tests,
            TestQueryFilter filters
        )
        {
            tests = tests.Where(test => test.UserId == filters.UserId);

            if (filters.TestType != null)
            {
                tests = tests.Where(test => test.TestType == filters.TestType);
            }

            if (filters.Difficulty != null)
            {
                tests = tests.Where(test => test.Difficulty == filters.Difficulty);
            }

            if (filters.FromDate != null)
            {
                tests = tests.Where(test => test.CreatedOn.Date >= filters.FromDate.Value.Date);
            }

            if (filters.ToDate != null)
            {
                tests = tests.Where(test => test.CreatedOn.Date <= filters.ToDate.Value.Date);
            }

            return tests;
        }
    }
}
