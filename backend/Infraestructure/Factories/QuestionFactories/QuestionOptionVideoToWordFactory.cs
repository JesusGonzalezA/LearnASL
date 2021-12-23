using System;
using Core.Entities.Tests;

namespace Infraestructure.Factories.QuestionFactories
{
    public class QuestionOptionVideoToWordFactory : QuestionFactory
    {
        public override QuestionOptionVideoToWordEntity CreateQuestion()
        {
            return new QuestionOptionVideoToWordEntity
            {
                VideoToGuess = "",
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
