using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Interfaces;
using Core.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenOptions _tokenOptions;

        public TokenService(IOptions<TokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;
        }

        public string GenerateJWTToken(string email = null, string guid = null)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            List<Claim> userClaims = new List<Claim>();
            if (email != null)
            {
                userClaims.Add(new Claim(ClaimTypes.Email, email));
            }
            if(guid != null)
            {
                userClaims.Add(new Claim(ClaimTypes.NameIdentifier, guid));
            }

            claims.AddRange(userClaims);

            return Generate(claims);
        }

        private string Generate(List<Claim> claims)
        {
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
