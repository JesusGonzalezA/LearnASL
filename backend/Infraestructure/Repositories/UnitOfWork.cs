using System.Threading.Tasks;
using Core.Interfaces;
using Infraestructure.Data;

namespace Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private readonly IUserRepository _userRepository;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}