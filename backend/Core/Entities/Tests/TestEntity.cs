using System;
using System.Collections.Generic;
using Core.Entities.Tests;
using Core.Enums;

namespace Core.Entities.Tests
{
    public class TestEntity<T> : BaseEntity where T : BaseQuestionEntity
    {
        public Difficulty Difficulty { get; set; }
        public int NumberOfQuestions { get; set; }
        public bool IsErrorTest { get; set; }
        public Guid UserGuid { get; set; }
        //public virtual UserEntity User { get; set; }
        public virtual ICollection<BaseQuestionEntity> Questions { get; set; }

        public TestEntity()
        {
            Initialize();
        }

        public TestEntity
        (
            Difficulty difficulty,
            int numberOfQuestions,
            bool isErrorTest,
            Guid userGuid
        )
        {
            Initialize(userGuid, difficulty, numberOfQuestions, isErrorTest);
        }

        private void Initialize
        (
            Guid userGuid = default(Guid),
            Difficulty difficulty = Difficulty.UNDEFINED,
            int numberOfQuestions = 0,
            bool isErrorTest = false
        )
        {
            Difficulty = difficulty;
            NumberOfQuestions = numberOfQuestions;
            IsErrorTest = isErrorTest;
            UserGuid = userGuid;
        }
    }
}
