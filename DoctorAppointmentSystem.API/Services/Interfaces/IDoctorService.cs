using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;
using DoctorAppointmentSystem.Shared.Enums;

namespace DoctorAppointmentSystem.API.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorResponseDto>> SearchDoctorsAsync(DoctorSearchFilterDto filter);

        Task<IEnumerable<TimeSlot>> GetAvailableSlotsAsync(GetAvailableSlotRequestDto request);
    }
}
