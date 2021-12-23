using System;

namespace Core.Entities.Tests
{
    public class QuestionOptionVideoToWordEntity : BaseEntity, IQuestion
    {
        public Guid TestId { get; set; }
        public virtual TestOptionVideoToWordEntity Test { get; set; }

        public string VideoToGuess { get; set; }
        public string PossibleAnswer0 { get; set; }
        public string PossibleAnswer1 { get; set; }
        public string PossibleAnswer2 { get; set; }
        public string PossibleAnswer3 { get; set; }
        public string UserAnswer { get; set; }
        public string CorrectAnswer { get; set; }

        public bool IsReplied()
        {
            return UserAnswer != null;
        }

        public bool IsCorrect()
        {
            return IsReplied() && CorrectAnswer.Equals(UserAnswer);
        }
    }
}
