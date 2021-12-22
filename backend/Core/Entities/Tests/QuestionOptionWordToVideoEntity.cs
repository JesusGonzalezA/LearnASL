using System;

namespace Core.Entities.Tests
{
    public class QuestionOptionWordToVideoEntity : BaseEntity, IQuestion
    {
        public string WordToGuess { get; set; }
        public string PossibleAnswer0 { get; set; }
        public string PossibleAnswer1 { get; set; }
        public string PossibleAnswer2 { get; set; }
        public string PossibleAnswer3 { get; set; }
        public string UserAnswer { get; set; }
        public string CorrectAnswer { get; set; }
        public Guid TestId { get; set; }

        public virtual TestOptionWordToVideoEntity Test { get; set; }

        public bool IsCorrect()
        {
            return (CorrectAnswer.Equals(UserAnswer)) && UserAnswer != null;
        }
    }
}
