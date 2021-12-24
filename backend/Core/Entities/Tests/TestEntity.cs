using System;
using Core.Enums;

namespace Core.Entities.Tests
{
    public class TestEntity : BaseEntity
    {
        public Difficulty Difficulty { get; set; }
        public TestType TestType { get; set; }
        public Guid UserId { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
