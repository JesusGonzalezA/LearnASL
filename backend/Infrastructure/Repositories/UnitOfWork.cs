using System.Threading.Tasks;
using Core.Entities.Tests;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        private readonly IUserRepository _userRepository;

        private readonly IDatasetRepository _datasetRepository;

        private readonly ILearntWordRepository _learntWordRepository;
        private readonly IErrorWordRepository _errorWordRepository;

        private readonly ITestRepository _testRepository;

        private readonly IBaseRepository<QuestionOptionWordToVideoEntity> _questionOptionWordToVideoRepository;
        private readonly IBaseRepository<QuestionOptionVideoToWordEntity> _questionOptionVideoToWordRepository;
        private readonly IBaseRepository<QuestionMimicEntity> _questionMimicRepository;
        private readonly IBaseRepository<QuestionQAEntity> _questionQARepository;

        public UnitOfWork(DatabaseContext context, IDatasetRepository datasetRepository = null)
        {
            _context = context;

            _datasetRepository = datasetRepository;
        }

        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);

        public IDatasetRepository DatasetRepository
                => _datasetRepository ?? new DatasetRepository(_context);

        public ILearntWordRepository LearntWordRepository
                => _learntWordRepository ?? new LearntWordRepository(_context);
        public IErrorWordRepository ErrorWordRepository
                => _errorWordRepository ?? new ErrorWordRepository(_context);

        public ITestRepository TestRepository
                => _testRepository ?? new TestRepository(_context);

        public IBaseRepository<QuestionOptionWordToVideoEntity> QuestionOptionWordToVideoRepository
                => _questionOptionWordToVideoRepository ?? new BaseRepository<QuestionOptionWordToVideoEntity>(_context);

        public IBaseRepository<QuestionOptionVideoToWordEntity> QuestionOptionVideoToWordRepository
                => _questionOptionVideoToWordRepository ?? new BaseRepository<QuestionOptionVideoToWordEntity>(_context);

        public IBaseRepository<QuestionMimicEntity> QuestionMimicRepository
                => _questionMimicRepository ?? new BaseRepository<QuestionMimicEntity>(_context);

        public IBaseRepository<QuestionQAEntity> QuestionQARepository
                => _questionQARepository ?? new BaseRepository<QuestionQAEntity>(_context);

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