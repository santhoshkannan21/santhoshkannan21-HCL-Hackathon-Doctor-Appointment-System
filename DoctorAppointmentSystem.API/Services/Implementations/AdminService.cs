using DoctorAppointmentSystem.API.Data;
using DoctorAppointmentSystem.API.Services.Interfaces;
using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;
using DoctorAppointmentSystem.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentSystem.API.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;
        public AdminService(AppDbContext context) => _context = context;

        public async Task<DailySummaryResponseDto> GetDailySummaryAsync(DailySummaryFilterDto filter)
        {
            var dayAppts = await _context.Appointments
                .Include(a => a.Doctor)
                .Where(a => a.AppointmentDate.Date == filter.Date.Date && a.Status != AppointmentStatus.Cancelled)
                .ToListAsync();

            return new DailySummaryResponseDto
            {
                SummaryDate = filter.Date,
                TotalAppointments = dayAppts.Count,
                TotalRevenue = dayAppts.Sum(a => a.TotalAmount),
                OnlineCount = dayAppts.Count(a => a.Doctor.Mode == ConsultationMode.Online),
                OfflineCount = dayAppts.Count(a => a.Doctor.Mode == ConsultationMode.Offline)
            };
        }
    }
}
