using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enums;

namespace Core.Interfaces
{
    public interface IDatasetRepository : IBaseRepository<VideoEntity>
    {
        Task<IList<VideoEntity> > GetVideosFromDataset(int numberOfVideos, Difficulty difficulty);
        Task<IList<VideoEntity>> GetVideosFromDataset(int numberOfVideos, string skipWord);
    }
}
