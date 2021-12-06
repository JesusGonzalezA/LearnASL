
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEmailService
    {
        Task SendRegistrationEmail(string email);
        Task SendPasswordRecoveryEmail(string email);
    }
}
