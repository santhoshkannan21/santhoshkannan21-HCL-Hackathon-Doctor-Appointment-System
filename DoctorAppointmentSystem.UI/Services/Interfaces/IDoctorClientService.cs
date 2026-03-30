using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;
using DoctorAppointmentSystem.Shared.Enums;

namespace DoctorAppointmentSystem.UI.Services.Interfaces
{
    public interface IDoctorClientService
    {
        Task<IEnumerable<DoctorResponseDto>> SearchDoctorsAsync(DoctorSearchFilterDto filter);

        Task<IEnumerable<TimeSlot>> GetAvailableSlotsAsync(GetAvailableSlotRequestDto request);
    }
}
