using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;
using DoctorAppointmentSystem.UI.Helpers;
using DoctorAppointmentSystem.UI.Services.Interfaces;

namespace DoctorAppointmentSystem.UI.Services.Implementations;

public class AppointmentClientService : IAppointmentClientService
{
    private readonly HttpClient _http;
    private readonly ProtectedLocalStorage _storage;

    public AppointmentClientService(HttpClient http, ProtectedLocalStorage storage)
    {
        _http = http;
        _storage = storage;
    }

    public async Task<AppointmentResponseDto> BookAppointmentAsync(BookAppointmentRequestDto request)
    {
        await _http.SetAuthorizationHeader(_storage);
        var response = await _http.PostAsJsonAsync("api/appointments/book", request);
        return await response.Content.ReadFromJsonAsync<AppointmentResponseDto>() ?? new();
    }

    public async Task<AppointmentResponseDto> GetAppointmentByIdAsync(int id)
    {
        await _http.SetAuthorizationHeader(_storage);
        return await _http.GetFromJsonAsync<AppointmentResponseDto>($"api/appointments/{id}") ?? new();
    }

    public async Task<bool> UpdateStatusAsync(int id, UpdateStatusRequestDto request)
    {
        await _http.SetAuthorizationHeader(_storage);
        var response = await _http.PutAsJsonAsync($"api/appointments/{id}/status", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CancelAppointmentAsync(int id)
    {
        await _http.SetAuthorizationHeader(_storage);
        var response = await _http.PutAsync($"api/appointments/{id}/cancel", null);
        return response.IsSuccessStatusCode;
    }
}