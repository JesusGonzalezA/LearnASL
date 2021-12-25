using System;
using Core.Entities.Tests;
using Core.Enums;

namespace Infraestructure.Factories.QuestionFactories
{
    public class QuestionMimicFactory : QuestionFactory
    {
        public override QuestionMimicEntity CreateQuestion(Guid testId, Difficulty difficulty)
        {
            return new QuestionMimicEntity
            {
                TestId = testId,
                WordToGuess = "",
                VideoUser = null,
                VideoHelp = "",
                IsCorrect = false
            };
        }
    }
}