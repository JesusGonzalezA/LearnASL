using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Tests;
using Core.Enums;
using Core.Options;

namespace Infrastructure.Factories.QuestionFactories
{
    public abstract class QuestionFactory
    {
        protected string BaseDirVideos { get; set; }

        public void Initialize(VideoServingOptions videoServingOptions)
        {
            BaseDirVideos = $"{videoServingOptions.ServerUrl}{videoServingOptions.Route}/{videoServingOptions.WLASLDirectory}";
        }

        public abstract BaseQuestionEntity CreateQuestion(
            Guid testId,
            Difficulty difficulty,
            DatasetItemEntity toGuess,
            IList<DatasetItemEntity> possibleAnswers = null
        );
    }
}