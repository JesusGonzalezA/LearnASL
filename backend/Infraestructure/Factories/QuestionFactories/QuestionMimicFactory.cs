using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Tests;
using Core.Enums;

namespace Infraestructure.Factories.QuestionFactories
{
    public class QuestionMimicFactory : QuestionFactory
    {
        public override QuestionMimicEntity CreateQuestion
        (
            Guid testId,
            Difficulty difficulty,
            VideoEntity toGuess,
            IList<VideoEntity>? possibleAnswers
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