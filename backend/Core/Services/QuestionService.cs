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
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddQuestions(TestType testType, IEnumerable<BaseQuestionEntity> questions)
        {
            switch (testType)
            {
                case TestType.OptionWordToVideo:
                case TestType.OptionWordToVideo_Error:
                    foreach(BaseQuestionEntity question in questions)
                        await _unitOfWork.QuestionOptionWordToVideoRepository.Add((QuestionOptionWordToVideoEntity)question);
                    break;

                case TestType.OptionVideoToWord:
                case TestType.OptionVideoToWord_Error:
                    foreach (BaseQuestionEntity question in questions)
                        await _unitOfWork.QuestionOptionVideoToWordRepository.Add((QuestionOptionVideoToWordEntity)question);
                    break;

                case TestType.QA:
                case TestType.QA_Error:
                    foreach (BaseQuestionEntity question in questions)
                        await _unitOfWork.QuestionQARepository.Add((QuestionQAEntity)question);
                    break;

                case TestType.Mimic:
                case TestType.Mimic_Error:
                    foreach (BaseQuestionEntity question in questions)
                        await _unitOfWork.QuestionMimicRepository.Add((QuestionMimicEntity)question);
                    break;

                default:
                    throw new Exception("Invalid test type");
            }
        }

        public async Task<IEnumerable<BaseQuestionEntity> > GetQuestions(TestEntity test)
        {
            IEnumerable<BaseQuestionEntity> questions = test.TestType switch
            {
                TestType.OptionWordToVideo or TestType.OptionWordToVideo_Error
                    => await _unitOfWork.QuestionOptionWordToVideoRepository.GetAll(),

                TestType.OptionVideoToWord or TestType.OptionVideoToWord_Error
                    => await _unitOfWork.QuestionOptionVideoToWordRepository.GetAll(),

                TestType.QA or TestType.QA_Error
                => await _unitOfWork.QuestionQARepository.GetAll(),

                TestType.Mimic or TestType.Mimic_Error
                    => await _unitOfWork.QuestionMimicRepository.GetAll(),

                _
                    => throw new Exception("Invalid test type"),
            };
            IEnumerable<BaseQuestionEntity> questionsFromTest
                = questions.Where(question => question.TestId == test.Id);

            return questionsFromTest;
        }

        public async Task UpdateQuestion(TestType testType, Guid questionGuid, UpdateQuestionParameters parameters)
        {
            switch (testType)
            {
                case TestType.OptionWordToVideo:
                case TestType.OptionWordToVideo_Error:
                    await UpdateQuestionOptionWordToVideo(questionGuid, parameters.UserAnswer);
                    break;

                case TestType.OptionVideoToWord:
                case TestType.OptionVideoToWord_Error:
                    await UpdateQuestionOptionVideoToWord(questionGuid, parameters.UserAnswer);
                    break;

                case TestType.QA:
                case TestType.QA_Error:
                    await UpdateQuestionQA(questionGuid, parameters.VideoUser);
                    break;

                case TestType.Mimic:
                case TestType.Mimic_Error:
                    await UpdateQuestionMimic(questionGuid, parameters.VideoUser);
                    break;

                default:
                    throw new Exception("Invalid test type");
            }
        }

        private async Task UpdateQuestionMimic(Guid questionGuid, string videoUser)
        {
            QuestionMimicEntity question = (QuestionMimicEntity)await GetQuestion(TestType.Mimic, questionGuid);
            question.VideoUser = videoUser;
            await _unitOfWork.QuestionMimicRepository.Update(question);
        }

        private async Task UpdateQuestionQA(Guid questionGuid, string videoUser)
        {
            QuestionQAEntity question = (QuestionQAEntity)await GetQuestion(TestType.QA, questionGuid);
            question.VideoUser = videoUser;
            await _unitOfWork.QuestionQARepository.Update(question);
        }

        private async Task UpdateQuestionOptionVideoToWord(Guid questionGuid, string userAnswer)
        {
            QuestionOptionVideoToWordEntity question = (QuestionOptionVideoToWordEntity)await GetQuestion(TestType.OptionVideoToWord, questionGuid);
            question.UserAnswer = userAnswer;
            await _unitOfWork.QuestionOptionVideoToWordRepository.Update(question);
        }

        private async Task UpdateQuestionOptionWordToVideo(Guid questionGuid, string userAnswer)
        {
            QuestionOptionWordToVideoEntity question = (QuestionOptionWordToVideoEntity) await GetQuestion(TestType.OptionWordToVideo, questionGuid);
            question.UserAnswer = userAnswer;
            await _unitOfWork.QuestionOptionWordToVideoRepository.Update(question);
        }

        public async Task<BaseQuestionEntity> GetQuestion(TestType testType, Guid guid)
        {
            BaseQuestionEntity question = null;

            switch (testType)
            {
                case TestType.OptionWordToVideo:
                case TestType.OptionWordToVideo_Error:
                    question = await _unitOfWork.QuestionOptionWordToVideoRepository.GetById(guid);
                    break;

                case TestType.OptionVideoToWord:
                case TestType.OptionVideoToWord_Error:
                    question = await _unitOfWork.QuestionOptionVideoToWordRepository.GetById(guid);
                    break;

                case TestType.QA:
                case TestType.QA_Error:
                    question = await _unitOfWork.QuestionQARepository.GetById(guid);
                    break;

                case TestType.Mimic:
                case TestType.Mimic_Error:
                    question = await _unitOfWork.QuestionMimicRepository.GetById(guid);
                    break;
            }

            if (question == null)
            {
                throw new BusinessException("Question does not exist");
            }

            return question;
        }
    }
}
