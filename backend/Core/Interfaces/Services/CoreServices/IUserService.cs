using System;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<UserEntity> GetUser(Guid id);
        Task<UserEntity> GetUserByEmail(string email);
        Task<Guid> AddUser(UserEntity user);
        Task DeleteUser(Guid id);
        Task<bool> CheckConfirmedUser(string email);
        Task ChangePassword(string email, string password, string token);
        Task ConfirmEmail(string email, string token);
        Task UpdateTokenEmailConfirmation(string email, string token);
        Task UpdateTokenPasswordRecovery(string email, string token);
        Task ChangeEmail(string oldEmail, string newEmail);
    }
}
