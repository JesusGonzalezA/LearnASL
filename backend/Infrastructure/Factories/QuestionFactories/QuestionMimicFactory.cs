using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Tests;
using Core.Enums;

namespace Infrastructure.Factories.QuestionFactories
{
    public class QuestionMimicFactory : QuestionFactory
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
                DatasetItemId = toGuess.Id,
                WordToGuess = toGuess.Word,
                VideoUser = null,
                VideoHelp = $"{BaseDirVideos}/{toGuess.VideoFilename}",
                IsCorrect = false
            };
        }
    }
}