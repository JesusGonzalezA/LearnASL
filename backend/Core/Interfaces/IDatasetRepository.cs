using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enums;

namespace Core.Interfaces
{
    public interface IDatasetRepository : IBaseRepository<DatasetItemEntity>
    {
        Task<IList<DatasetItemEntity> > GetVideosFromDataset(int numberOfVideos, Difficulty difficulty);
        Task<IList<DatasetItemEntity> > GetVideosFromDataset(int numberOfVideos, string skipWord);
        Task<int> GetSizeOfDataset();
    }
}
