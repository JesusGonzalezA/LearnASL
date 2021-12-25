using System.Collections.Generic;

namespace Core.Entities.Tests
{
    public class TestWithQuestions : TestEntity
    {
        public virtual IEnumerable<BaseQuestionEntity> Questions { get; set; }
    }
}
