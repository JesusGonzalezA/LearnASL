
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailConfirmationEmail(string email, string token);
        Task SendPasswordRecoveryEmail(string email, string token);
    }
}
