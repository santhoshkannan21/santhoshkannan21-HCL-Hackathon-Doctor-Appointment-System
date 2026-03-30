using DoctorAppointmentSystem.API.Data;
using DoctorAppointmentSystem.API.Entities;
using DoctorAppointmentSystem.API.Services.Interfaces;
using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;
using DoctorAppointmentSystem.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentSystem.API.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppDbContext _context;

        public AppointmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AppointmentResponseDto> BookAppointmentAsync(BookAppointmentRequestDto request, int patientId)
        {
            var doctor = await _context.Doctors.FindAsync(request.DoctorId)
                         ?? throw new Exception("Doctor not found.");

            var exists = await _context.Appointments.AnyAsync(a =>
                a.DoctorId == request.DoctorId &&
                a.AppointmentDate.Date == request.AppointmentDate.Date &&
                a.TimeSlot == request.TimeSlot &&
                a.Status != AppointmentStatus.Cancelled);

            if (exists) throw new Exception("This slot is already booked! Please select another time.");

            var appointment = new Appointment
            {
                PatientId = patientId,
                DoctorId = request.DoctorId,
                AppointmentDate = request.AppointmentDate,
                TimeSlot = request.TimeSlot,
                Status = AppointmentStatus.Booked,
                TotalAmount = doctor.ConsultationFee,
                ModeArtifact = doctor.Mode == ConsultationMode.Online
                    ? $"https://meet.hcl.com/room-{doctor.Id}-{Guid.NewGuid().ToString().Substring(0, 5)}"
                    : $"Apollo Clinic, Floor {doctor.Id}, Room {100 + doctor.Id}"
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return await GetAppointmentByIdAsync(appointment.Id);
        }

        public async Task<AppointmentResponseDto> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.Doctor.Specialty)
                .Select(a => new AppointmentResponseDto
                {
                    Id = a.Id,
                    DoctorName = a.Doctor.Name,
                    PatientName = a.Patient.FullName,
                    SpecialtyName = a.Doctor.Specialty.Name,
                    AppointmentDate = a.AppointmentDate,
                    TimeSlot = a.TimeSlot,
                    Status = a.Status,
                    ModeArtifact = a.ModeArtifact,
                    TotalAmount = a.TotalAmount
                }).FirstOrDefaultAsync(a => a.Id == id)!;
        }

        public async Task<AppointmentResponseDto> UpdateStatusAsync(int id, UpdateStatusRequestDto request)
        {
            var appt = await _context.Appointments.FindAsync(id)
                       ?? throw new Exception("Appointment record not found.");

            appt.Status = request.NewStatus;
            await _context.SaveChangesAsync();

            return await GetAppointmentByIdAsync(id);
        }

        public async Task<AppointmentResponseDto> CancelAppointmentAsync(int id, int patientId)
        {
            // Security check: Only the patient who booked it can cancel it
            var appt = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == id && a.PatientId == patientId)
                ?? throw new Exception("Unauthorized or appointment not found.");

            appt.Status = AppointmentStatus.Cancelled;
            await _context.SaveChangesAsync();

            return await GetAppointmentByIdAsync(id);
        }
    }
}
