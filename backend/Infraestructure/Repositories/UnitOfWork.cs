using System.Threading.Tasks;
using Core.Entities.Tests;
using Core.Interfaces;
using Infraestructure.Data;

namespace Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IBaseRepository<QuestionOptionWordToVideoEntity> _questionOptionWordToVideoRepository;
        private readonly IBaseRepository<TestOptionWordToVideoEntity> _testOptionWordToVideoRepository;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);

        public IBaseRepository<QuestionOptionWordToVideoEntity> QuestionOptionWordToVideoRepository
                => _questionOptionWordToVideoRepository ?? new BaseRepository<QuestionOptionWordToVideoEntity>(_context);

        public IBaseRepository<TestOptionWordToVideoEntity> TestOptionWordToVideoRepository
                => _testOptionWordToVideoRepository ?? new BaseRepository<TestOptionWordToVideoEntity>(_context);

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