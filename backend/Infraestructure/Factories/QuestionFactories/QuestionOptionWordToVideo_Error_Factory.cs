using System;
using Core.Entities.Tests;

namespace Infraestructure.Factories.QuestionFactories
{
    public class QuestionOptionWordToVideo_Error_Factory : QuestionFactory
    {
        public override QuestionOptionWordToVideoEntity CreateQuestion()
        {
            return new QuestionOptionWordToVideoEntity
            {
                WordToGuess = "",
                PossibleAnswer0 = "",
                PossibleAnswer1 = "",
                PossibleAnswer2 = "",
                PossibleAnswer3 = "",
                UserAnswer = "",
                CorrectAnswer = "",
                TestId = Guid.Empty
            };
        }
    }
}
