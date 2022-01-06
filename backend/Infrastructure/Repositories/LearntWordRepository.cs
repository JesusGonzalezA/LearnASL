using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LearntWordRepository : BaseRepository<LearntWordEntity>, ILearntWordRepository
    {
        public LearntWordRepository(DatabaseContext context) : base(context) { }

        public Task<LearntWordEntity> Get(Guid userId, Guid datasetItemId)
        {
            return _entities
                .FirstOrDefaultAsync(l => l.UserId == userId && l.DatasetItemEntityId == datasetItemId);
        }

        public async Task<int> GetNumberOfWordsLearntByUser(Guid userId)
        {
            return await _entities
                    .Where(l => l.UserId == userId)
                    .CountAsync();
        }
    }
}
