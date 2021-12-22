using System;
using System.Collections.Generic;
using Core.Entities.Tests;
using Core.Enums;
using Infraestructure.QuestionFactories;

namespace Infraestructure.TestFactories
{
    public class TestOptionWordToVideoFactory : TestFactory
    {
        public TestOptionWordToVideoFactory()
        {
            _questionFactory = new QuestionOptionWordToVideoFactory();
        }

        public override ITest CreateTest
        (
            TestType testType,
            Difficulty difficulty,
            int numberOfQuestions
        )
        {
            // Create questions
            ICollection<IQuestion> questions = new List<IQuestion>();

            for (int i = 0; i < numberOfQuestions; ++i)
            {
                questions.Add(_questionFactory.CreateQuestion());
            }

            // Create test
            return new TestOptionWordToVideoEntity
            {
                Difficulty = difficulty,
                NumberOfQuestions = numberOfQuestions,
                IsErrorTest = (testType == TestType.OptionWordToVideoEntity_Error),
                Questions = questions
            };
        }
    }
}
