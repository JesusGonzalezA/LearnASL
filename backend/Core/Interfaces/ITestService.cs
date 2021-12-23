using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Tests;
using Core.Enums;

namespace Core.Interfaces
{
    public interface ITestService
    {
        Task<ITest> GetTest(TestType testType, Guid guid);
        Task<Guid> AddTest(TestType testType, ITest test);
        Task DeleteTest(TestType testType, Guid guid);
        Task<IEnumerable<ITest> > GetAllTests(Guid userGuid);
    }
}
