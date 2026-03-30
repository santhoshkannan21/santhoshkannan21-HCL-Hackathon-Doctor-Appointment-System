using DoctorAppointmentSystem.Shared.Enums;

namespace DoctorAppointmentSystem.API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public Doctor? DoctorProfile { get; set; }
    }
}
