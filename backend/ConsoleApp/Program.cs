using System;
using System.Collections.Generic;
using Core.Entities.Tests;
using Core.Enums;
using Infraestructure.Factories.TestFactories;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TestFactory testFactory  = new TestOptionWordToVideoFactory();
            TestFactory testFactory1 = new TestOptionVideoToWordFactory();

            ITest test  = testFactory.CreateTest(Difficulty.EASY, 5);
            ITest test1 = testFactory1.CreateTest(Difficulty.EASY, 5);

            ICollection<ITest> tests = new List<ITest>();
            tests.Add(test);
            tests.Add(test1);
        }
    }
}
