using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.Contracts.Incoming
{
    public class TestCreateDto
    {
        [Required]
        public int NumberOfQuestions { get; set; }

        public TestType? TestType { get; set; }

        public Difficulty? Difficulty { get; set; }
    }
}
