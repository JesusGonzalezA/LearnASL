using System;
using Core.Entities.Tests;
using Core.Enums;

namespace Infraestructure.Factories.QuestionFactories
{
    public class QuestionMimic_Error_Factory : QuestionFactory
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