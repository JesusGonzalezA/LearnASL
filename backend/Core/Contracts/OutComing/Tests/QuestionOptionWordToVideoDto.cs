
using System.ComponentModel.DataAnnotations;

namespace Core.Contracts.OutComing.Tests
{
    public class QuestionOptionWordToVideoDto : BasePopulatedQuestionDto
    {
        [Required]
        public string WordToGuess { get; set; }

        [Required]
        public string PossibleAnswer0 { get; set; }

        [Required]
        public string PossibleAnswer1 { get; set; }

        [Required]
        public string PossibleAnswer2 { get; set; }

        [Required]
        public string PossibleAnswer3 { get; set; }

        [Required]
        public string UserAnswer { get; set; }

        [Required]
        public string CorrectAnswer { get; set; }
    }
}
