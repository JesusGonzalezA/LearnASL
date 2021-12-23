using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Contracts.OutComing.Tests
{
    public class TestOptionWordToVideoDto : ITestDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public new ICollection<QuestionOptionWordToVideoDto> Questions { get; set; }
    }
}
