using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.Contracts.Incoming
{
    public class TestCreateDto
    {
        [Required]
        public Difficulty Difficulty { get; set; }

        [Required]
        public int NumberOfQuestions { get; set; }
    }
}
