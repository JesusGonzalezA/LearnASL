using Core.Entities.Tests;

namespace Core.Helpers
{
    public static class TestHelper
    {
        public static bool CheckIsFinished(ITest test)
        {
            foreach (IQuestion question in test.Questions)
            {
                if (!question.IsReplied())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
