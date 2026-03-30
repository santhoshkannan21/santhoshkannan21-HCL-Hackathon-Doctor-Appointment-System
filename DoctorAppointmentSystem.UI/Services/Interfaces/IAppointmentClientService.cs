using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;

namespace DoctorAppointmentSystem.UI.Services.Interfaces
{
    public interface IAppointmentClientService
    {
        Task<AppointmentResponseDto> BookAppointmentAsync(BookAppointmentRequestDto request);

        Task<AppointmentResponseDto> GetAppointmentByIdAsync(int id);

        Task<bool> CancelAppointmentAsync(int id);

        Task<bool> UpdateStatusAsync(int id, UpdateStatusRequestDto request);
    }
}
