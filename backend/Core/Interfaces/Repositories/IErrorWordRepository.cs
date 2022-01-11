using System;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IErrorWordRepository : IBaseRepository<ErrorWordEntity>
    {
        Task<ErrorWordEntity> Get(Guid userId, Guid datasetItemId);
    }
}
