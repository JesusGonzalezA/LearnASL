using Core.Entities.Tests;
using Core.Enums;
using Infraestructure.Factories.QuestionFactories;

namespace Infraestructure.Factories.TestFactories
{
    public abstract class TestFactory
    {
        protected QuestionFactory _questionFactory { get; set; }

        public abstract ITest CreateTest(Difficulty difficulty, int numberOfQuestions);
    }
}
