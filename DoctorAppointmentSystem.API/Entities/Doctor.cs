using DoctorAppointmentSystem.Shared.Enums;

namespace DoctorAppointmentSystem.API.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int UserId { get; set; }

        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; } = null!;

        public ConsultationMode Mode { get; set; }
        public decimal ConsultationFee { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public User User { get; set; } = null!;
    }
}
