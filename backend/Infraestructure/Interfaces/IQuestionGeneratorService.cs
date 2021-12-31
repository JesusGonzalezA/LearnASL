using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Tests;
using Core.Enums;

namespace Infraestructure.Interfaces
{
    public interface IQuestionGeneratorService
    {
        public Task<IList<BaseQuestionEntity> > CreateQuestions(int numberOfQuestions, TestType testType, Difficulty difficulty, Guid testId);
    }
}