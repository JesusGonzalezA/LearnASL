using System;
using Core.Entities.Tests;
using Infraestructure.TestFactories;

namespace Infraestructure.QuestionFactories
{
    public class QuestionOptionWordToVideoFactory : QuestionFactory
    {
        public override IQuestion CreateQuestion()
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
