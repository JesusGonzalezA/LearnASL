using System.Collections.Generic;

namespace Core.Entities.Tests
{
    public class OptionWordToVideoQuestionEntity : BaseQuestionEntity
    {
        public string WordToGuess { get; set; }
        public List<string> PossibleAnswers { get; set; }
        public int IndexOfUserAnswer { get; set; }
        public int IndexOfCorrectAnswer { get; set; }
        public virtual TestEntity<OptionWordToVideoQuestionEntity> Test { get; set; }

        public OptionWordToVideoQuestionEntity()
        {
            Initialize();
        }

        public OptionWordToVideoQuestionEntity
        (
            string wordToGuess,
            List<string> possibleAnswers,
            int indexOfUserAnswer,
            int indexOfCorrectAnswer,
            TestEntity<OptionWordToVideoQuestionEntity> test
        )
        {
            Initialize
            (
                wordToGuess,
                possibleAnswers,
                indexOfUserAnswer,
                indexOfCorrectAnswer,
                test
            );
        }


        private void Initialize
        (
            string wordToGuess = "",
            List<string> possibleAnswers = null,
            int indexOfUserAnswer = -1,
            int indexOfCorrectAnswer = -1,
            TestEntity<OptionWordToVideoQuestionEntity> test = null
        )
        {
            WordToGuess = wordToGuess;
            PossibleAnswers = possibleAnswers ?? new List<string>() { "", "", "", "" };
            IndexOfUserAnswer = indexOfUserAnswer;
            IndexOfCorrectAnswer = indexOfCorrectAnswer;
            Test = test;
        }

        public override bool IsCorrect()
        {
            return (IndexOfUserAnswer == IndexOfCorrectAnswer)
                && (IndexOfCorrectAnswer != -1);
        }
    }
}
