using System;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ILearntWordRepository : IBaseRepository<LearntWordEntity>
    {
        Task<LearntWordEntity> Get(Guid userId, Guid datasetItemId);
    }
}
