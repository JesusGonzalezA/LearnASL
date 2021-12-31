﻿using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Tests;
using Core.Enums;

namespace Infraestructure.Factories.QuestionFactories
{
    public class QuestionOptionWordToVideoFactory : QuestionFactory
    {
        public override QuestionOptionWordToVideoEntity CreateQuestion
        (
            Guid testId,
            Difficulty difficulty,
            VideoEntity toGuess,
            IList<VideoEntity>? possibleAnswers
        )
        {
            return new QuestionOptionWordToVideoEntity
            {
                WordToGuess = toGuess.Word,
                PossibleAnswer0 = $"{BaseDirVideos}/{possibleAnswers[0]?.VideoFilename}",
                PossibleAnswer1 = $"{BaseDirVideos}/{possibleAnswers[1]?.VideoFilename}",
                PossibleAnswer2 = $"{BaseDirVideos}/{possibleAnswers[2]?.VideoFilename}",
                PossibleAnswer3 = $"{BaseDirVideos}/{possibleAnswers[3]?.VideoFilename}",
                UserAnswer = null,
                CorrectAnswer = $"{BaseDirVideos}/{toGuess.VideoFilename}",
                TestId = testId
            };
        }
    }
}