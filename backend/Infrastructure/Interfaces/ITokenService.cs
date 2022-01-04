
namespace Infrastructure.Interfaces
{
    public interface ITokenService
    {
        dynamic GenerateJWTToken(string email, string guid);
        string GenerateJWTToken();
    }
}
