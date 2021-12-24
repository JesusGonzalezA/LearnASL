using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Contracts.OutComing.Tests
{
    public class PopulatedTestDto : TestDto
    {
        [Required]
        public new IEnumerable<BasePopulatedQuestionDto> Questions { get; set; } 
    }
}
