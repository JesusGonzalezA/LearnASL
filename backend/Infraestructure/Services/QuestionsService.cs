using System;
using System.Collections.Generic;
using Core.Entities.Tests;
using Core.Enums;
using Infraestructure.Interfaces;

namespace Infraestructure.Services
{
    public class QuestionsService : IQuestionsService
    {
        public QuestionsService()
        {
        }

        public ICollection<QuestionOptionWordToVideoEntity> GenerateQuestionsOptionWordToVideoEntity
        (
            Guid testId,
            int numberOfQuestions,
            bool isErrorTest
        )
        {
            ICollection<QuestionOptionWordToVideoEntity> questions = new List<QuestionOptionWordToVideoEntity>();

            for (int i=0; i<numberOfQuestions; ++i)
            {
                QuestionOptionWordToVideoEntity question = new QuestionOptionWordToVideoEntity
                {
                    TestId = testId,
                    CorrectAnswer = "",
                    WordToGuess = "",
                    PossibleAnswer0 = "",
                    PossibleAnswer1 = "",
                    PossibleAnswer2 = "",
                    PossibleAnswer3 = "",
                    UserAnswer = ""
                };
                questions.Add(question);
            }
            return questions;
        }
    }
}
