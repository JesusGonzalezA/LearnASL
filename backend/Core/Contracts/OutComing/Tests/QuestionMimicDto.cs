using System.ComponentModel.DataAnnotations;

namespace Core.Contracts.OutComing.Tests
{
    public class QuestionMimicDto : BasePopulatedQuestionDto
    {
        [Required]
        public string WordToGuess { get; set; }

        [Required]
        public string VideoHelp { get; set; }

        [Required]
        public string VideoUser { get; set; }

        [Required]
        public bool IsCorrect { get; set; }
    }
}
