using System;
using Core.Entities.Tests;
using Core.Enums;

namespace Infraestructure.Factories.QuestionFactories
{
    public class QuestionOptionVideoToWordFactory : QuestionFactory
    {
        public override QuestionOptionVideoToWordEntity CreateQuestion(Guid testId, Difficulty difficulty)
        {
            return new QuestionOptionVideoToWordEntity
            {
                VideoToGuess = "",
                PossibleAnswer0 = "",
                PossibleAnswer1 = "",
                PossibleAnswer2 = "",
                PossibleAnswer3 = "",
                UserAnswer = null,
                CorrectAnswer = "",
                TestId = testId
            };
        }
    }
}