using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;

namespace DoctorAppointmentSystem.API.Services.Interfaces
{
    public interface IAdminService
    {
        Task<DailySummaryResponseDto> GetDailySummaryAsync(DailySummaryFilterDto filter);
    }
}
