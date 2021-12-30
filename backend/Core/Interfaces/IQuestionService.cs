using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.CustomEntities;
using Core.Entities.Tests;
using Core.Enums;

namespace Core.Interfaces
{
    public interface IQuestionService
    {
        Task AddQuestions(TestType testType, IEnumerable<BaseQuestionEntity> questions);
        Task<IEnumerable<BaseQuestionEntity> > GetQuestions(TestEntity test);
        Task<BaseQuestionEntity> GetQuestion(TestType testType, Guid guid);
        Task UpdateQuestion(TestType testType, Guid questionGuid, UpdateQuestionParameters parameters);
    }
}
