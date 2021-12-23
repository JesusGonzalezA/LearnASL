using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.Contracts.OutComing
{
    public class ITestDto
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
        public ICollection<IQuestionDto> Questions { get; set; }
    }
}
