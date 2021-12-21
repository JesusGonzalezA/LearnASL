using System;
namespace Core.Entities.Tests
{
    public abstract class BaseQuestionEntity : BaseEntity
    {
        public abstract bool IsCorrect();
    }
}
