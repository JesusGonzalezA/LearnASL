using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Tests;
using Core.Enums;

namespace Infrastructure.Factories.QuestionFactories
{
    public class QuestionMimic_Error_Factory : QuestionFactory
    {
        public override QuestionMimicEntity CreateQuestion
        (
            Guid testId,
            Difficulty difficulty,
            DatasetItemEntity toGuess,
            IList<DatasetItemEntity>? possibleAnswers
        )
        {
            return new QuestionMimicEntity
            {
                TestId = testId,
                WordToGuess = toGuess.Word,
                VideoUser = null,
                VideoHelp = $"{BaseDirVideos}/{toGuess.VideoFilename}",
                IsCorrect = false
            };
        }
    }
}