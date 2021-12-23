using System;
using Core.Entities.Tests;
using Core.Enums;
using Infraestructure.Factories.TestFactories;
using Infraestructure.Interfaces;

namespace Infraestructure.Services
{
    public class TestGenerator : ITestGenerator
    {
        public ITest CreateTest(TestType testType, Difficulty difficulty, int numberOfQuestions)
        {
            TestFactory testFactory = testType switch
            {
                TestType.OptionWordToVideoEntity => new TestOptionWordToVideoFactory(),
                TestType.OptionWordToVideoEntity_Error => new TestOptionWordToVideo_Error_Factory(),
                TestType.OptionVideoToWordEntity => new TestOptionVideoToWordFactory(),
                TestType.OptionVideoToWordEntity_Error => new TestOptionVideoToWord_Error_Factory(),
                _ => throw new Exception("Unable to create factory. Invalid test type"),
            };

            ITest test = testFactory.CreateTest(difficulty, numberOfQuestions);
            return test;
        }
    }
}
