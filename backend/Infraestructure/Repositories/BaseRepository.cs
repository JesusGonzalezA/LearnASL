using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DatabaseContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(DatabaseContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T> > GetAll()
        {
            return await _entities.ToListAsync();
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return _entities;
        }

        public async Task<T> GetById(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<Guid> Add(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(Guid id)
        {
            T entity = await GetById(id);
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
