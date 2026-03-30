using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;
using DoctorAppointmentSystem.UI.Security;
using DoctorAppointmentSystem.UI.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace DoctorAppointmentSystem.UI.Services.Implementations;

public class AuthClientService : IAuthClientService
{
    private readonly HttpClient _http;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ProtectedLocalStorage _storage;

    public AuthClientService(HttpClient http, AuthenticationStateProvider authStateProvider, ProtectedLocalStorage storage)
    {
        _http = http;
        _authStateProvider = authStateProvider;
        _storage = storage;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", request);
        if (!response.IsSuccessStatusCode) throw new Exception("Invalid Credentials");

        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        // Native .NET Protected Storage
        await _storage.SetAsync("authToken", result!.Token);

        ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Token);
        return result;
    }

    public async Task<bool> RegisterAsync(RegisterRequestDto request)
    {
        var response = await _http.PostAsJsonAsync("api/auth/register", request);
        return response.IsSuccessStatusCode;
    }

    public async Task LogoutAsync()
    {
        await _storage.DeleteAsync("authToken");
        ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
    }
}