﻿using System;
using Core.Entities.Tests;
using Core.Enums;
using Infraestructure.Factories.QuestionFactories;
using Infraestructure.Interfaces;

namespace Infraestructure.Services
{
    public class QuestionGeneratorService : IQuestionGeneratorService
    {
        public BaseQuestionEntity CreateQuestion(TestType testType, Difficulty difficulty, Guid testId)
        {
            QuestionFactory questionFactory = testType switch
            {
                TestType.OptionWordToVideo => new QuestionOptionWordToVideoFactory(),
                TestType.OptionWordToVideo_Error => new QuestionOptionWordToVideo_Error_Factory(),

                TestType.OptionVideoToWord => new QuestionOptionVideoToWordFactory(),
                TestType.OptionVideoToWord_Error => new QuestionOptionVideoToWord_Error_Factory(),

                TestType.QA => new QuestionQAFactory(),
                TestType.QA_Error => new QuestionQA_Error_Factory(),

                TestType.Mimic => new QuestionMimicFactory(),
                TestType.Mimic_Error => new QuestionMimic_Error_Factory(),

                _ => throw new Exception("Unable to create factory. Invalid test type"),
            };

            return questionFactory.CreateQuestion(testId, difficulty);
        }
    }
}