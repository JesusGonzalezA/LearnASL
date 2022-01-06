using System;
using System.Threading.Tasks;
using Core.Entities.Tests;
using Core.QueryFilters;
using Core.CustomEntities;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface ITestService
    {
        Task<TestEntity> GetTest(Guid guid);
        Task<Guid> AddTest(TestEntity test);
        Task DeleteTest(Guid guid);
        PagedList<TestWithQuestions> GetAllTestsPaged(TestQueryFilter filters);
        IList<TestWithQuestions> GetAllTests(TestQueryFilter filters);
        Task DeleteAllTestsFromUser(Guid userId);
    }
}