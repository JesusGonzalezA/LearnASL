using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.Tests;
using Core.QueryFilters;

namespace Core.Extensions
{
    public static class IEnumerableOfTestEntity_Filter_TestQueryFilter
    {
        public static IEnumerable<TestEntity>
        Filter
        (
            this IEnumerable<TestEntity> tests,
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
                tests = tests.Where(test => test.CreatedOn.Date >= filters.FromDate?.Date);
            }

            if (filters.ToDate != null)
            {
                tests = tests.Where(test => test.CreatedOn.Date <= filters.ToDate?.Date);
            }

            return tests;
        }
    }
}
