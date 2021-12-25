using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Tests;
using Core.Enums;
using Core.Interfaces;
using System.Linq;

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
                case TestType.OptionWordToVideoEntity:
                case TestType.OptionWordToVideoEntity_Error:
                    foreach(BaseQuestionEntity question in questions)
                        await _unitOfWork.QuestionOptionWordToVideoRepository.Add((QuestionOptionWordToVideoEntity)question);
                    break;

                case TestType.OptionVideoToWordEntity:
                case TestType.OptionVideoToWordEntity_Error:
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
                TestType.OptionWordToVideoEntity or TestType.OptionWordToVideoEntity_Error
                    => await _unitOfWork.QuestionOptionWordToVideoRepository.GetAll(),

                TestType.OptionVideoToWordEntity or TestType.OptionVideoToWordEntity_Error
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
    }
}
