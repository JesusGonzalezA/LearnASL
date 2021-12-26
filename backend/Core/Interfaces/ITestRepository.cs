using System;
using System.Threading.Tasks;
using Core.Entities.Tests;

namespace Core.Interfaces
{
    public interface ITestRepository : IBaseRepository<TestEntity>
    {
        Task DeleteAllTestsFromUser(Guid userId);
    }
}
