﻿using System;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Tests;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }

        IBaseRepository<VideoEntity> DatasetRepository { get; }

        ITestRepository TestRepository { get; }

        IBaseRepository<QuestionOptionWordToVideoEntity> QuestionOptionWordToVideoRepository { get; }
        IBaseRepository<QuestionOptionVideoToWordEntity> QuestionOptionVideoToWordRepository { get; }
        IBaseRepository<QuestionMimicEntity> QuestionMimicRepository { get; }
        IBaseRepository<QuestionQAEntity> QuestionQARepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
