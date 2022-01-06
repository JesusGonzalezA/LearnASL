using System;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Tests;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }

        IDatasetRepository DatasetRepository { get; }

        ILearntWordRepository LearntWordRepository { get; }

        ITestRepository TestRepository { get; }

        IBaseRepository<QuestionOptionWordToVideoEntity> QuestionOptionWordToVideoRepository { get; }
        IBaseRepository<QuestionOptionVideoToWordEntity> QuestionOptionVideoToWordRepository { get; }
        IBaseRepository<QuestionMimicEntity> QuestionMimicRepository { get; }
        IBaseRepository<QuestionQAEntity> QuestionQARepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
