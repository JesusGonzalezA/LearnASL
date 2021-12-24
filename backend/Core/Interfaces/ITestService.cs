using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Tests;
using Core.Enums;

namespace Core.Interfaces
{
    public interface ITestService
    {
        Task<TestEntity> GetTest(Guid guid);
        Task<Guid> AddTest(TestEntity test);
        Task DeleteTest(Guid guid);
        Task<IEnumerable<TestEntity> > GetAllTests(Guid userGuid);
    }
}