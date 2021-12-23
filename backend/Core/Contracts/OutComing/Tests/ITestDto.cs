using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Contracts.OutComing.Tests
{
    public class ITestDto : ISimpleTestDto
    {
        [Required]
        public ICollection<IQuestionDto> Questions { get; set; }
    }
}
