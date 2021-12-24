using System;
using Core.Entities.Tests;
using Core.Enums;

namespace Infraestructure.Factories.QuestionFactories
{
    public abstract class QuestionFactory
    {
        public abstract BaseQuestionEntity CreateQuestion(Guid testId, Difficulty difficulty);
    }
}