using DevFreela.Core.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DevFreela.Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJWTToken(string email, string role)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];
            var expires = _configuration["Jwt:Expires"];

            var keyBytes = Encoding.UTF8.GetBytes(key);

            var securityKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim("userName", email));
            claims.Add(new Claim(ClaimTypes.Role, role));


            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.Now.AddHours(int.Parse(expires)),
                signingCredentials: credentials,
                claims: claims);

            var tokenHandler = new JwtSecurityTokenHandler();

            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }

        public string ComputeSha256Hash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
