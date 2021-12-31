using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Tests;
using Core.Enums;

namespace Infraestructure.Factories.QuestionFactories
{
    public class QuestionOptionVideoToWord_Error_Factory : QuestionFactory
    {
        public override QuestionOptionVideoToWordEntity CreateQuestion
        (
            Guid testId,
            Difficulty difficulty,
            VideoEntity toGuess,
            IList<VideoEntity>? possibleAnswers
        )
        {
            return new QuestionOptionVideoToWordEntity
            {
                VideoToGuess = $"{BaseDirVideos}/{toGuess.VideoFilename}",
                PossibleAnswer0 = possibleAnswers[0]?.Word,
                PossibleAnswer1 = possibleAnswers[1]?.Word,
                PossibleAnswer2 = possibleAnswers[2]?.Word,
                PossibleAnswer3 = possibleAnswers[3]?.Word,
                UserAnswer = null,
                CorrectAnswer = toGuess.Word,
                TestId = testId
            };
        }
    }
}