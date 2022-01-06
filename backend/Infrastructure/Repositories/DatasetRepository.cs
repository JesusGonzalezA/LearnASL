using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DatasetRepository : BaseRepository<DatasetItemEntity>, IDatasetRepository
    {
        public DatasetRepository(DatabaseContext context) : base(context) { }

        public async Task<int> GetSizeOfDataset()
        {
            return await _entities.CountAsync();
        }

        public async Task<IList<DatasetItemEntity> > GetVideosFromDataset(int numberOfVideos, Difficulty difficulty)
        {
            return await _entities
                    .Where(video => video.Difficulty == difficulty)
                    .OrderBy(v => Guid.NewGuid())
                    .Take(numberOfVideos)
                    .ToListAsync();
        }

        public async Task<IList<DatasetItemEntity>> GetVideosFromDataset(int numberOfVideos, string skipWord)
        {
            return await _entities
                    .Where(video => !video.Word.Equals(skipWord))
                    .OrderBy(v => Guid.NewGuid())
                    .Take(numberOfVideos)
                    .ToListAsync();
        }
    }
}
