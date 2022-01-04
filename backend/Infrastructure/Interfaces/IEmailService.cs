
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailConfirmationEmail(string email, string token);
        Task SendPasswordRecoveryEmail(string email, string token);
    }
}
