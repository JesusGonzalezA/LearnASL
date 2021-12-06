
namespace Core.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool ConfirmedEmail { get; set; }
        public string? TokenPasswordRecovery { get; set; }
        public string? TokenEmailConfirmation { get; set; }

        public UserEntity()
        {
            ConfirmedEmail = false;
            TokenPasswordRecovery = null;
            TokenEmailConfirmation = null;
        }
    }
}
