using Core.Entities.Tests;
using Core.Enums;

namespace Infraestructure.TestFactories
{
    public abstract class TestFactory
    {
        protected QuestionFactory _questionFactory { get; set; }

        public abstract ITest CreateTest(TestType testType, Difficulty difficulty, int numberOfQuestions);
    }
}
