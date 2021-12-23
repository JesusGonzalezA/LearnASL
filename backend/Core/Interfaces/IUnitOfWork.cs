using System;
using System.Threading.Tasks;
using Core.Entities.Tests;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }

        IBaseRepository<QuestionOptionWordToVideoEntity> QuestionOptionWordToVideoRepository { get; }
        IBaseRepository<TestOptionWordToVideoEntity> TestOptionWordToVideoRepository { get; }

        IBaseRepository<QuestionOptionVideoToWordEntity> QuestionOptionVideoToWordRepository { get; }
        IBaseRepository<TestOptionVideoToWordEntity> TestOptionVideoToWordRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
