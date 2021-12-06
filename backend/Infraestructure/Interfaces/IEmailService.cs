
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailConfirmationEmail(string email, string token);
        Task SendPasswordRecoveryEmail(string email, string token);
    }
}
