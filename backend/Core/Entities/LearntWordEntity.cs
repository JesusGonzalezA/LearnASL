using System;

namespace Core.Entities
{
    public class LearntWordEntity : BaseEntity
    {
        public Guid DatasetItemEntityId { get; set; }
        public Guid UserId { get; set; }

        public virtual DatasetItemEntity DatasetItem { get; set; }
        public virtual UserEntity User { get; set; }
    }
}
