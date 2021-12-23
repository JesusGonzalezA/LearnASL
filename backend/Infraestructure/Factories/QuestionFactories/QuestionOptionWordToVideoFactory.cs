using System;
using Core.Entities.Tests;

namespace Infraestructure.Factories.QuestionFactories
{
    public class QuestionOptionWordToVideoFactory : QuestionFactory
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
                UserAnswer = null,
                CorrectAnswer = "",
                TestId = Guid.Empty
            };
        }
    }
}
