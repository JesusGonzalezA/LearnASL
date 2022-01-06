
namespace Core.Entities.Tests
{
    public class QuestionMimicEntity : BaseQuestionEntity
    {
        public string WordToGuess { get; set; }
        public string VideoHelp { get; set; }
        public string VideoUser { get; set; }
        public bool IsCorrect { get; set; }

        public override bool IsQuestionCorrect()
        {
            return IsCorrect;
        }
    }
}
