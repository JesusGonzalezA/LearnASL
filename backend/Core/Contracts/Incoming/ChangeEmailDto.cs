using System.ComponentModel.DataAnnotations;

namespace Core.Contracts.Incoming
{
    public class ChangeEmailDto
    {
        [Required]
        public string Email { get; set; }
    }
}
