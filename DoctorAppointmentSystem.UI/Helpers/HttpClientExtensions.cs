using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace DoctorAppointmentSystem.UI.Helpers;

public static class HttpClientExtensions
{
    public static async Task SetAuthorizationHeader(this HttpClient client, ProtectedLocalStorage storage)
    {
        try
        {
            var result = await storage.GetAsync<string>("authToken");
            if (result.Success && !string.IsNullOrWhiteSpace(result.Value))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Value);
            }
        }
        catch
        {
            client.DefaultRequestHeaders.Authorization = null;
        }
    }
}