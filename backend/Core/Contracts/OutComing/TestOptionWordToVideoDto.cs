using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Contracts.Common;
using Core.Enums;

namespace Core.Contracts.OutComing
{
    public class TestOptionWordToVideoDto : BaseDto
    {
        [Required]
        public Difficulty Difficulty { get; set; }

        [Required]
        public int NumberOfQuestions { get; set; }

        [Required]
        public bool IsErrorTest { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public ICollection<QuestionOptionWordToVideoDto> Questions { get; set; }
    }
}
