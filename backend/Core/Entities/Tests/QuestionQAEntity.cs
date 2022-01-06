
namespace Core.Entities.Tests
{
    public class QuestionQAEntity : BaseQuestionEntity
    {
        public string WordToGuess { get; set; }
        public bool IsCorrect { get; set; }
        public string VideoUser { get; set; }

        public override bool IsQuestionCorrect()
        {
            return IsCorrect;
        }
    }
}
