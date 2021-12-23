using System.Collections.Generic;
using Core.Entities.Tests;
using Core.Enums;
using Infraestructure.Factories.QuestionFactories;

namespace Infraestructure.Factories.TestFactories
{
    public class TestOptionVideoToWord_Error_Factory : TestFactory
    {
        public TestOptionVideoToWord_Error_Factory()
        {
            _questionFactory = new QuestionOptionVideoToWord_Error_Factory();
        }

        public override TestOptionVideoToWordEntity CreateTest
        (
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
            return new TestOptionVideoToWordEntity
            {
                Difficulty = difficulty,
                NumberOfQuestions = numberOfQuestions,
                IsErrorTest = true,
                Questions = questions
            };
        }
    }
}
