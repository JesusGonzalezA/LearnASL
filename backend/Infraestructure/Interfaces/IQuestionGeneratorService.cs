using System;
using Core.Entities.Tests;
using Core.Enums;

namespace Infraestructure.Interfaces
{
    public interface IQuestionGeneratorService
    {
        public BaseQuestionEntity CreateQuestion(TestType testType, Difficulty difficulty, Guid testId);
    }
}