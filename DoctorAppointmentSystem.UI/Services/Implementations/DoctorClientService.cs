using System.Net.Http.Json;
using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;
using DoctorAppointmentSystem.Shared.Enums;
using DoctorAppointmentSystem.UI.Services.Interfaces;

namespace DoctorAppointmentSystem.UI.Services.Implementations;

public class DoctorClientService : IDoctorClientService
{
    private readonly HttpClient _http;
    public DoctorClientService(HttpClient http) => _http = http;

    public async Task<IEnumerable<DoctorResponseDto>> SearchDoctorsAsync(DoctorSearchFilterDto filter)
    {
        var url = $"api/doctors/search?Mode={(int)filter.Mode}&SpecialtyId={filter.SpecialtyId}";
        return await _http.GetFromJsonAsync<IEnumerable<DoctorResponseDto>>(url) ?? new List<DoctorResponseDto>();
    }

    public async Task<IEnumerable<TimeSlot>> GetAvailableSlotsAsync(GetAvailableSlotRequestDto request)
    {
        var url = $"api/doctors/available-slots?DoctorId={request.DoctorId}&Date={request.Date:yyyy-MM-dd}";
        return await _http.GetFromJsonAsync<IEnumerable<TimeSlot>>(url) ?? new List<TimeSlot>();
    }
}