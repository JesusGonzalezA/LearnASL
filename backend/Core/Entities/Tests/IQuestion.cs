
using System;

namespace Core.Entities.Tests
{
    public interface IQuestion
    {
        public Guid TestId { get; set; }
        public bool IsCorrect();
    }
}
