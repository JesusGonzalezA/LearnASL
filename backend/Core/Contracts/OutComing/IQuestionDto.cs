using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Contracts.OutComing
{
    public class IQuestionDto
    {
        [Required]
        public Guid TestId { get; set; }
    }
}
