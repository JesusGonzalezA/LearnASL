using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Contracts.OutComing.Tests
{
    public class TestOptionVideoToWordDto : ITestDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public new ICollection<QuestionOptionVideoToWordDto> Questions { get; set; }
    }
}
