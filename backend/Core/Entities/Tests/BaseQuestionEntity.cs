using System;
namespace Core.Entities.Tests
{
    public abstract class BaseQuestionEntity : BaseEntity
    {
        public Guid TestId { get; set; }

        public virtual TestEntity Test { get; set; }
    }
}
