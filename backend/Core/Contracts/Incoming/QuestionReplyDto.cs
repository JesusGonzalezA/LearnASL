using System.ComponentModel.DataAnnotations;
using Core.Enums;
using Core.ValidationAttributes;
using Microsoft.AspNetCore.Http;

namespace Core.Contracts.Incoming
{
    public class QuestionReplyDto
    {
        [Required]
        public TestType TestType { get; set; }

        public string? UserAnswer { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".mp4" })]
        public IFormFile? VideoUser { get; set; }
    }
}
