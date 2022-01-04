using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        IQueryable<T> GetAllAsQueryable();
        Task<T> GetById(Guid id);
        Task<Guid> Add(T entity);
        Task Delete(Guid id);
        Task Update(T entity);
    }
}
