
using System.Collections.Generic;
using Core.Entities.Tests;
using Core.Enums;
using Infraestructure.Factories.QuestionFactories;

namespace Infraestructure.Factories.TestFactories
{
    public class TestOptionWordToVideo_Error_Factory : TestFactory
    {
        public TestOptionWordToVideo_Error_Factory()
        {
            _questionFactory = new QuestionOptionWordToVideo_Error_Factory();
        }

        public override TestOptionWordToVideoEntity CreateTest
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
            return new TestOptionWordToVideoEntity
            {
                Difficulty = difficulty,
                NumberOfQuestions = numberOfQuestions,
                IsErrorTest = false,
                Questions = questions
            };
        }
    }
}
