using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infraestructure.Interfaces;
using Infraestructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infraestructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenOptions _tokenOptions;

        public TokenService(IOptions<TokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;
        }

        public dynamic GenerateJWTToken(string email)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey)
                        ),
                        SecurityAlgorithms.HmacSha256
                    )
                ),
                new JwtPayload(claims)
            );

            var output = new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = email
            };

            return output;
        }

        public string GenerateJWTToken()
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey)
                        ),
                        SecurityAlgorithms.HmacSha256
                    )
                ),
                new JwtPayload(claims)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
