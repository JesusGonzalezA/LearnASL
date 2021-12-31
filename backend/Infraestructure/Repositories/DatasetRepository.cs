using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class DatasetRepository : BaseRepository<VideoEntity>, IDatasetRepository
    {
        public DatasetRepository(DatabaseContext context) : base(context) { }

        public async Task<IList<VideoEntity> > GetVideosFromDataset(int numberOfVideos, Difficulty difficulty)
        {
            return await _entities
                    .Where(video => video.Difficulty == difficulty)
                    .OrderBy(v => Guid.NewGuid())
                    .Take(numberOfVideos)
                    .ToListAsync();
        }

        public async Task<IList<VideoEntity>> GetVideosFromDataset(int numberOfVideos, string skipWord)
        {
            return await _entities
                    .Where(video => !video.Word.Equals(skipWord))
                    .OrderBy(v => Guid.NewGuid())
                    .Take(numberOfVideos)
                    .ToListAsync();
        }
    }
}
