﻿using System;
using System.Collections.Generic;
using Core.Enums;

namespace Core.Entities.Tests
{
    public class TestOptionWordToVideoEntity : BaseEntity, ITest {
        public Difficulty Difficulty { get; set; }
        public int NumberOfQuestions { get; set; }
        public bool IsErrorTest { get; set; }
        public Guid UserId { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual ICollection<IQuestion> Questions { get; set; }
    }
}
