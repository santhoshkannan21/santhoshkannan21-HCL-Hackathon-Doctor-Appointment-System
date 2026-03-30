using DoctorAppointmentSystem.API.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoctorAppointmentSystem.API.Helpers
{
    public static class AuthHelper
    {

        public static string GenerateJwtToken(User user, IConfiguration config)
        {
            var secretKey = config["JwtSettings:SecretKey"] ?? throw new InvalidOperationException("JWT Secret is missing!");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));


            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = credentials,
                Issuer = config["JwtSettings:Issuer"],
                Audience = config["JwtSettings:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        public static string HashPassword(string plainTextPassword)
        {

            return BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
        }


        public static bool VerifyPassword(string plainTextPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
        }
    }
}
