using System.Collections.Generic;
using Core.Entities.Tests;

namespace Core.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool ConfirmedEmail { get; set; }
        public string? TokenPasswordRecovery { get; set; }
        public string? TokenEmailConfirmation { get; set; }
        public virtual ICollection<ITest> Tests { get; set; }

        public UserEntity()
        {
            Initialize();
        }

        public UserEntity(string email, string password)
        {
            Initialize(email, password);
        }

        private void Initialize(string email = "", string password = "")
        {
            Email = email;
            Password = password;
            ConfirmedEmail = false;
            TokenPasswordRecovery = null;
            TokenEmailConfirmation = null;
        }
    }
}
