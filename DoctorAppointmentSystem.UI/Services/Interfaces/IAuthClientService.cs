using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;

namespace DoctorAppointmentSystem.UI.Services.Interfaces
{
    public interface IAuthClientService
    {
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
        Task<bool> RegisterAsync(RegisterRequestDto request);
        Task LogoutAsync();
    }
}
