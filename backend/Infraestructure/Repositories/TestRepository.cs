using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Tests;
using Core.Interfaces;
using Infraestructure.Data;

namespace Infraestructure.Repositories
{
    public class TestRepository : BaseRepository<TestEntity>, ITestRepository
    {
        public TestRepository(DatabaseContext context) : base(context) { }

        public async Task DeleteAllTestsFromUser(Guid userId)
        {
            _entities.RemoveRange(_entities.Where(test => test.UserId == userId));
            await _context.SaveChangesAsync();
        }
    }
}
