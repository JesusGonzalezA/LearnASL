using System;
using System.Collections.Generic;
using Core.Enums;

namespace Core.Entities.Tests
{
    public interface ITest
    {
        public Difficulty Difficulty { get; set; }
        public int NumberOfQuestions { get; set; }
        public bool IsErrorTest { get; set; }
        public Guid UserId { get; set; }

        public UserEntity User { get; set; }
        public ICollection<IQuestion> Questions { get; set; }
    }
}
