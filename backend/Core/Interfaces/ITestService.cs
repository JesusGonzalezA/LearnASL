using System;
using System.Threading.Tasks;
using Core.Entities.Tests;

namespace Core.Interfaces
{
    public interface ITestService
    {
        Task<TestOptionWordToVideoEntity> GetTest(Guid id);
        Task<Guid> AddTest(TestOptionWordToVideoEntity test);
        Task DeleteTest(Guid guid);
    }
}
