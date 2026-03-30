using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;
using DoctorAppointmentSystem.UI.Helpers;
using DoctorAppointmentSystem.UI.Services.Interfaces;

namespace DoctorAppointmentSystem.UI.Services.Implementations;

public class AdminClientService : IAdminClientService
{
    private readonly HttpClient _http;
    private readonly ProtectedLocalStorage _storage;

    public AdminClientService(HttpClient http, ProtectedLocalStorage storage)
    {
        _http = http;
        _storage = storage;
    }

    public async Task<DailySummaryResponseDto> GetDailySummaryAsync(DailySummaryFilterDto filter)
    {
        await _http.SetAuthorizationHeader(_storage);
        var url = $"api/admin/daily-summary?Date={filter.Date:yyyy-MM-dd}";
        return await _http.GetFromJsonAsync<DailySummaryResponseDto>(url) ?? new();
    }
}