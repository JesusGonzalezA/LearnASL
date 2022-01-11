using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Tests;
using Core.Enums;

namespace Core.Interfaces
{
    public interface IQuestionGeneratorService
    {
        public Task<IList<BaseQuestionEntity> > CreateQuestions
        (
            int numberOfQuestions,
            TestType testType,
            Difficulty difficulty,
            Guid testId,
            Guid userId
        );
    }
}