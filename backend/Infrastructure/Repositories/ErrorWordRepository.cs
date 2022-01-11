using System;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ErrorWordRepository : BaseRepository<ErrorWordEntity>, IErrorWordRepository
    {
        public ErrorWordRepository(DatabaseContext context) : base(context) { }

        public Task<ErrorWordEntity> Get(Guid userId, Guid datasetItemId)
        {
            return _entities
                .FirstOrDefaultAsync(l => l.UserId == userId && l.DatasetItemEntityId == datasetItemId);
        }
    }
}
