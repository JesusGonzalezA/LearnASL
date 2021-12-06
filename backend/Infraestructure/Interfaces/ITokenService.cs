﻿
namespace Infraestructure.Interfaces
{
    public interface ITokenService
    {
        dynamic GenerateJWTToken(string email);
        string GenerateJWTToken();
    }
}
