using System;
using Core.Entities.Tests;
using Core.Enums;

namespace Infraestructure.Factories.QuestionFactories
{
    public class QuestionOptionWordToVideo_Error_Factory : QuestionFactory
    {
        public override QuestionOptionWordToVideoEntity CreateQuestion(Guid testId, Difficulty difficulty)
        {
            return new QuestionOptionWordToVideoEntity
            {
                WordToGuess = "",
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
