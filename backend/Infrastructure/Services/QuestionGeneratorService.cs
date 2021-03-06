using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Tests;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;
using Core.Options;
using Infrastructure.Factories.QuestionFactories;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services
{
    public partial class QuestionGeneratorService : IQuestionGeneratorService
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

        public async Task<IList<BaseQuestionEntity> > CreateQuestions
        (
            int numberOfQuestions,
            TestType testType,
            Difficulty difficulty,
            Guid testId,
            Guid userId
        )
        {
            IList<DatasetItemEntity> toGuessArr = await GetToGuessArr(numberOfQuestions, testType, difficulty, userId);
            IList<List<DatasetItemEntity>> possibleAnswersArr = await GetPossibleAnswersArr(testType, numberOfQuestions, toGuessArr);
            
            List<BaseQuestionEntity> questions = new();
            for (int i = 0; i < numberOfQuestions; ++i)
            {
                questions.Add(
                    CreateQuestion(
                        testType,
                        difficulty,
                        testId,
                        toGuessArr[i],
                        possibleAnswersArr?.ElementAt(i)
                    )
                );
            }

            return questions;
        }

        
    }

    public partial class QuestionGeneratorService
    {
        private BaseQuestionEntity CreateQuestion
        (
            TestType testType,
            Difficulty difficulty,
            Guid testId,
            DatasetItemEntity toGuess,
            IList<DatasetItemEntity> possibleAnswers = null
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

        private async Task<IList<DatasetItemEntity>> GetToGuessArr
        (
            int numberOfQuestions,
            TestType testType,
            Difficulty difficulty,
            Guid? userId
        )
        {
            IList<DatasetItemEntity> toGuessArr;

            // Error questions
            if (testType == TestType.OptionWordToVideo_Error
                            || testType == TestType.OptionVideoToWord_Error
                            || testType == TestType.Mimic_Error
                            || testType == TestType.QA_Error
            )
            {
                if (userId == null)
                {
                    throw new Exception("User id cannot be null when creating error tests");
                }

                IQueryable<ErrorWordEntity> errorWordEntities = _unitOfWork
                    .ErrorWordRepository
                    .GetAllAsQueryable()
                    .Where(w => w.UserId == userId)
                    .Where(w => w.DatasetItem.Difficulty == difficulty);

                if (numberOfQuestions > errorWordEntities.Count())
                    throw new BusinessException("Not enough error questions.");

                toGuessArr = errorWordEntities
                    .OrderBy(w => Guid.NewGuid())
                    .Take(numberOfQuestions)
                    .Select(w => w.DatasetItem)
                    .ToList();
            }

            // Non-Error questions
            else
            {
                toGuessArr = await _unitOfWork
                    .DatasetRepository
                    .GetVideosFromDataset(numberOfQuestions, difficulty);
            }

            return toGuessArr;
        }

        private async Task<IList<List<DatasetItemEntity> > > GetPossibleAnswersArr
        (
            TestType testType,
            int numberOfQuestions,
            IList<DatasetItemEntity> toGuessArr
        )
        {
            List<List<DatasetItemEntity>> possibleAnswersArr = null;

            if (testType == TestType.OptionVideoToWord
                || testType == TestType.OptionVideoToWord_Error
                || testType == TestType.OptionWordToVideo
                || testType == TestType.OptionWordToVideo_Error
            )
            {
                possibleAnswersArr = new();

                for (int i = 0; i<numberOfQuestions; ++i)
                {
                    DatasetItemEntity toGuess = toGuessArr.ElementAt(i);

                    string skipWord = toGuess.Word;
                    int numberOfAlternatives = 4;

                    IList<DatasetItemEntity> possibleAnswers = await _unitOfWork.DatasetRepository.GetVideosFromDataset(numberOfAlternatives - 1, skipWord);
                    possibleAnswers.Add(toGuess);
                                possibleAnswers = possibleAnswers.OrderBy(a => Guid.NewGuid()).ToList();

                    possibleAnswersArr.Add((List<DatasetItemEntity>) possibleAnswers);
                }
            }

            return possibleAnswersArr;
        }
    }
}