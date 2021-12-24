using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.Contracts.Incoming
{
    public class TestQueryFilterDto
    {
        [Required]
        public int PageSize { get; set; }

        [Required]
        public int PageNumber { get; set; }

        public TestType? TestType { get; set; }

        public Difficulty? Difficulty { get; set; }
    }
}
