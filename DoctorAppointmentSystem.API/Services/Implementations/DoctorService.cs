using DoctorAppointmentSystem.API.Data;
using DoctorAppointmentSystem.API.Services.Interfaces;
using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;
using DoctorAppointmentSystem.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentSystem.API.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly AppDbContext _context;

        public DoctorService(AppDbContext context) => _context = context;


        public async Task<IEnumerable<DoctorResponseDto>> SearchDoctorsAsync(DoctorSearchFilterDto filter)
        {
            return await _context.Doctors
                .Include(d => d.Specialty)
                .Where(d => d.Mode == filter.Mode && d.SpecialtyId == filter.SpecialtyId)
                .Select(d => new DoctorResponseDto
                {
                    Id = d.Id,
                    FullName = d.Name,
                    SpecialtyName = d.Specialty.Name,
                    Mode = d.Mode.ToString(),
                    ConsultationFee = d.ConsultationFee
                }).ToListAsync();
        }

        public async Task<IEnumerable<TimeSlot>> GetAvailableSlotsAsync(GetAvailableSlotRequestDto request)
        {
            var allSlots = Enum.GetValues<TimeSlot>().ToList();
            var bookedSlots = await _context.Appointments
                .Where(a => a.DoctorId == request.DoctorId &&
                            a.AppointmentDate.Date == request.Date.Date &&
                            a.Status != AppointmentStatus.Cancelled)
                .Select(a => a.TimeSlot)
                .ToListAsync();

            return allSlots.Except(bookedSlots);
        }
    }
}
