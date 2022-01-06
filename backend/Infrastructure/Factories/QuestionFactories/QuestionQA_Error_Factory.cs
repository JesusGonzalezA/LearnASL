using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Tests;
using Core.Enums;

namespace Infrastructure.Factories.QuestionFactories
{
    public class QuestionQA_Error_Factory : QuestionFactory
    {
        public override QuestionQAEntity CreateQuestion
        (
            Guid testId,
            Difficulty difficulty,
            DatasetItemEntity toGuess,
            IList<DatasetItemEntity>? possibleAnswers
        )
        {
            return new QuestionQAEntity
            {
                TestId = testId,
                WordToGuess = toGuess.Word,
                VideoUser = null,
                IsCorrect = false
            };
        }
    }
}