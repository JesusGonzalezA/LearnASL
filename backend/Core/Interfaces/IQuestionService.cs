using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Tests;
using Core.Enums;

namespace Core.Interfaces
{
    public interface IQuestionService
    {
        Task AddQuestions(TestType testType, IEnumerable<BaseQuestionEntity> questions);
        Task<IEnumerable<BaseQuestionEntity> > GetQuestions(TestEntity test);
    }
}
