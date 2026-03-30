using DoctorAppointmentSystem.Shared.Enums;

namespace DoctorAppointmentSystem.API.Entities
{
    public class Appointment
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public AppointmentStatus Status { get; set; }

        public string ModeArtifact { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }

        public User Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
    }
}
