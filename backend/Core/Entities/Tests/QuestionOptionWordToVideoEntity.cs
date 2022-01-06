
namespace Core.Entities.Tests
{
    public class QuestionOptionWordToVideoEntity : BaseQuestionEntity
    {
        public string WordToGuess { get; set; }
        public string PossibleAnswer0 { get; set; }
        public string PossibleAnswer1 { get; set; }
        public string PossibleAnswer2 { get; set; }
        public string PossibleAnswer3 { get; set; }
        public string UserAnswer { get; set; }
        public string CorrectAnswer { get; set; }

        public override bool IsQuestionCorrect()
        {
            if (UserAnswer == null) return false;

            return CorrectAnswer.Equals(UserAnswer);
        }
    }
}
