using DoctorAppointmentSystem.UI.Components;
using DoctorAppointmentSystem.UI.Services.Implementations;
using DoctorAppointmentSystem.UI.Services.Interfaces;
using DoctorAppointmentSystem.UI.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace DoctorAppointmentSystem.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. BLAZOR COMPONENTS SETUP
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // 2. HTTP CLIENT SETUP (Pointing to your Backend API)
            // Ensure the Port (7123 or 5000) matches your Backend launchSettings.json!
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7286/")
            });

            // 3. NATIVE PROTECTED STORAGE (Replaces the NuGet package)
            builder.Services.AddScoped<ProtectedLocalStorage>();

            // 4. AUTHENTICATION ENGINE
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddAuthorizationCore();

            // 5. CLIENT SERVICES INJECTION
            builder.Services.AddScoped<IAuthClientService, AuthClientService>();
            builder.Services.AddScoped<IDoctorClientService, DoctorClientService>();
            builder.Services.AddScoped<IAppointmentClientService, AppointmentClientService>();
            builder.Services.AddScoped<IAdminClientService, AdminClientService>();

            var app = builder.Build();

            // 6. MIDDLEWARE PIPELINE
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // .NET 10 uses UseStaticFiles for assets
            app.UseAntiforgery();

            // Map Components with Interactive Server Mode
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}