using System;
using System.ComponentModel.DataAnnotations;
using Core.Contracts.Common;

namespace Core.Contracts.OutComing
{
    public class QuestionOptionWordToVideoDto : BaseDto
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
