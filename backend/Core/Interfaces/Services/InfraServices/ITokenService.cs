
namespace Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateJWTToken(string email=null, string id=null);
    }
}
