using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;

namespace DoctorAppointmentSystem.API.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentResponseDto> BookAppointmentAsync(BookAppointmentRequestDto request, int patientId);

        Task<AppointmentResponseDto> GetAppointmentByIdAsync(int appointmentId);


        Task<AppointmentResponseDto> UpdateStatusAsync(int appointmentId, UpdateStatusRequestDto request);

        Task<AppointmentResponseDto> CancelAppointmentAsync(int appointmentId, int patientId);
    }
}
