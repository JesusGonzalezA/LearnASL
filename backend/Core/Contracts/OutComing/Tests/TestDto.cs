using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Contracts.Common;
using Core.Enums;

namespace Core.Contracts.OutComing.Tests
{
    public class TestDto : BaseDto
    {
        [Required]
        public Difficulty Difficulty { get; set; }

        [Required]
        public TestType TestType { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public IEnumerable<BaseQuestionDto> Questions { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
