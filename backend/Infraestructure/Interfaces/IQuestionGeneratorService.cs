using Core.Entities.Tests;
using Core.Enums;

namespace Infraestructure.Interfaces
{
    public interface ITestGenerator
    {
        public ITest CreateTest(TestType testType, Difficulty difficulty, int numberOfQuestions);
    }
}
