using System;
using System.ComponentModel.DataAnnotations;
using Core.Contracts.Common;
using Core.Enums;

namespace Core.Contracts.OutComing.Tests
{
    public class ISimpleTestDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Difficulty Difficulty { get; set; }

        [Required]
        public int NumberOfQuestions { get; set; }

        [Required]
        public bool IsErrorTest { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
