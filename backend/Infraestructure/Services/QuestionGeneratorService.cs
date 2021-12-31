using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Tests;
using Core.Enums;
using Core.Interfaces;
using Core.Options;
using Infraestructure.Factories.QuestionFactories;
using Infraestructure.Interfaces;
using Microsoft.Extensions.Options;

namespace Infraestructure.Services
{
    public class QuestionGeneratorService : IQuestionGeneratorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly VideoServingOptions _videoServingOptions;

        public QuestionGeneratorService
        (
            IUnitOfWork unitOfWork,
            IOptions<VideoServingOptions> videoServingOptions
        )
        {
            _unitOfWork = unitOfWork;
            _videoServingOptions = videoServingOptions.Value;
        }

        private BaseQuestionEntity CreateQuestion
        (
            TestType testType,
            Difficulty difficulty,
            Guid testId,
            VideoEntity toGuess,
            IList<VideoEntity> possibleAnswers = null
        )
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

            questionFactory.Initialize(_videoServingOptions);

            return questionFactory.CreateQuestion(testId, difficulty, toGuess, possibleAnswers);
        }

        public async Task<IList<BaseQuestionEntity> > CreateQuestions
        (
            int numberOfQuestions,
            TestType testType,
            Difficulty difficulty,
            Guid testId
        )
        {
            IList<VideoEntity> toGuessArr = await _unitOfWork.DatasetRepository.GetVideosFromDataset(numberOfQuestions, difficulty);
            List<List<VideoEntity>> possibleAnswersArr = null;

            if ( testType == TestType.OptionVideoToWord
                || testType == TestType.OptionVideoToWord_Error
                || testType == TestType.OptionWordToVideo
                || testType == TestType.OptionWordToVideo_Error
            )
            {
                possibleAnswersArr = new();

                for (int i = 0; i < numberOfQuestions; ++i)
                {
                    VideoEntity toGuess = toGuessArr.ElementAt(i);

                    string skipWord = toGuess.Word;
                    int numberOfAlternatives = 4;

                    IList<VideoEntity> possibleAnswers = await _unitOfWork.DatasetRepository.GetVideosFromDataset(numberOfAlternatives - 1, skipWord);
                    possibleAnswers.Add(toGuess);
                    possibleAnswers = possibleAnswers.OrderBy(a => Guid.NewGuid()).ToList();

                    possibleAnswersArr.Add((List<VideoEntity>)possibleAnswers);
                }
            }

            List<BaseQuestionEntity> questions = new();
            for (int i = 0; i < numberOfQuestions; ++i)
                questions.Add(
                    CreateQuestion(
                        testType,
                        difficulty,
                        testId,
                        toGuessArr[i],
                        possibleAnswersArr?.ElementAt(i)
                    )
                );
            return questions;
        }
    }
}