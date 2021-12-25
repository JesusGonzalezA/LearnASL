﻿using System;
using Core.Entities.Tests;
using Core.Enums;

namespace Infraestructure.Factories.QuestionFactories
{
    public class QuestionQAFactory : QuestionFactory
    {
        public override QuestionQAEntity CreateQuestion(Guid testId, Difficulty difficulty)
        {
            return new QuestionQAEntity
            {
                TestId = testId,
                WordToGuess = "",
                VideoUser = null,
                IsCorrect = false
            };
        }
    }
}