using DoctorAppointmentSystem.API.Data;
using DoctorAppointmentSystem.API.Entities;
using DoctorAppointmentSystem.API.Helpers;
using DoctorAppointmentSystem.API.Services.Interfaces;
using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentSystem.API.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || !AuthHelper.VerifyPassword(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password.");

            return new AuthResponseDto
            {
                Token = AuthHelper.GenerateJwtToken(user, _config),
                UserId = user.Id,
                FullName = user.FullName,
                Role = user.Role.ToString()
            };
        }

        public async Task<bool> RegisterAsync(RegisterRequestDto request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                throw new Exception("Email already exists.");

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = AuthHelper.HashPassword(request.Password),
                Role = request.Role
            };

            _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
