using System;
namespace Core.Entities.Tests
{
    public abstract class BaseQuestionEntity : BaseEntity
    {
        public Guid TestId { get; set; }
        public Guid DatasetItemId { get; set; }

        public virtual TestEntity Test { get; set; }
        public virtual DatasetItemEntity DatasetItem { get; set; }

        public abstract bool IsQuestionCorrect();
    }
}
