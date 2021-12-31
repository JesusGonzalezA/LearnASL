using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Tests;
using Core.Enums;
using Core.Options;

namespace Infraestructure.Factories.QuestionFactories
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
            VideoEntity toGuess,
            IList<VideoEntity> possibleAnswers = null
        );
    }
}