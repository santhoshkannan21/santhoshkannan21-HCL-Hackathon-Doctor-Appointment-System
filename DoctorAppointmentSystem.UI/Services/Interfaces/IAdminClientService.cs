using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;

namespace DoctorAppointmentSystem.UI.Services.Interfaces
{
    public interface IAdminClientService
    {
        Task<DailySummaryResponseDto> GetDailySummaryAsync(DailySummaryFilterDto filter);
    }
}
