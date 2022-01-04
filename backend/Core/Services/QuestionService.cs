using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Tests;
using Core.Enums;
using Core.Interfaces;
using System.Linq;
using Core.CustomEntities;
using Core.Exceptions;

namespace Core.Services
{
    public partial class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddQuestions(TestType testType, IEnumerable<BaseQuestionEntity> questions)
        {
            dynamic questionRepository = GetQuestionRepository(testType);

            foreach (dynamic question in questions)
            {
                await questionRepository.Add(question);
            }
        }

        public async Task<IEnumerable<BaseQuestionEntity> > GetQuestions(TestEntity test)
        {
            dynamic questionRepository = GetQuestionRepository(test.TestType);
            IEnumerable<BaseQuestionEntity> questions = await questionRepository.GetAll();

            IEnumerable<BaseQuestionEntity> questionsFromTest
                = questions.Where(question => question.TestId == test.Id);

            return questionsFromTest;
        }

        public async Task UpdateQuestion(TestType testType, Guid questionGuid, UpdateQuestionParameters parameters)
        {
            dynamic updatedQuestion = await GetUpdatedQuestion(testType, questionGuid, parameters);
            dynamic repository = GetQuestionRepository(testType);

            await repository.Update(updatedQuestion);
        }

        public async Task<BaseQuestionEntity> GetQuestion(TestType testType, Guid guid)
        {
            BaseQuestionEntity question = await GetQuestionRepository(testType).GetById(guid);

            if (question == null)
            {
                throw new BusinessException("Question does not exist");
            }

            return question;
        }
    }

    public partial class QuestionService
    {
        private dynamic GetQuestionRepository(TestType testType)
        {
            switch (testType)
            {
                case TestType.OptionWordToVideo:
                case TestType.OptionWordToVideo_Error:
                    return _unitOfWork.QuestionOptionWordToVideoRepository;

                case TestType.OptionVideoToWord:
                case TestType.OptionVideoToWord_Error:
                    return _unitOfWork.QuestionOptionVideoToWordRepository;

                case TestType.QA:
                case TestType.QA_Error:
                    return _unitOfWork.QuestionQARepository;

                case TestType.Mimic:
                case TestType.Mimic_Error:
                    return _unitOfWork.QuestionMimicRepository;

                default:
                    throw new BusinessException("Invalid test type");
            }
        }

        private async Task<dynamic> GetUpdatedQuestion(TestType testType, Guid questionGuid, UpdateQuestionParameters parameters)
        {
            dynamic question = await GetQuestion(testType, questionGuid);

            switch (testType)
            {
                case TestType.OptionWordToVideo:
                case TestType.OptionWordToVideo_Error:
                case TestType.OptionVideoToWord:
                case TestType.OptionVideoToWord_Error:
                    question.UserAnswer = parameters.UserAnswer;
                    break;

                case TestType.QA:
                case TestType.QA_Error:
                case TestType.Mimic:
                case TestType.Mimic_Error:
                    question.VideoUser = parameters.VideoUser;
                    break;
            }

            return question;
        }
    }
}
