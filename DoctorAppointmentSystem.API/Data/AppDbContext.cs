using DoctorAppointmentSystem.API.Entities;
using DoctorAppointmentSystem.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentSystem.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>()
                .HasIndex(a => new { a.DoctorId, a.AppointmentDate, a.TimeSlot })
                .IsUnique();

            modelBuilder.Entity<Doctor>()
                .Property(d => d.ConsultationFee)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Appointment>()
                .Property(a => a.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Specialty)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.User)
                .WithOne(u => u.DoctorProfile)
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Specialty>().HasData(
                new Specialty { Id = 1, Name = "General Physician" },
                new Specialty { Id = 2, Name = "Pediatrics" },
                new Specialty { Id = 3, Name = "Dermatology" },
                new Specialty { Id = 4, Name = "Gynecology" },
                new Specialty { Id = 5, Name = "Orthopedics" },
                new Specialty { Id = 6, Name = "Cardiology" },
                new Specialty { Id = 7, Name = "Neurology" }
            );


            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FullName = "System Admin", Email = "admin@hcl.com", PasswordHash = "$2a$12$TbRfUUFzgHdh0lFSWPu0CeWZ.GSj634SJrs8Ww1cnWcfNWwxhlDwe", Role = UserRole.Admin },
                new User { Id = 2, FullName = "Patient Rahul", Email = "rahul@test.com", PasswordHash = "$2a$12$hGOeop7IbSbMcljWnNESIef.vOQUaOUqDmn0B9NZrd1JQPhna1QIW", Role = UserRole.Patient },
                new User { Id = 3, FullName = "Patient Priya", Email = "priya@test.com", PasswordHash = "$2a$12$hGOeop7IbSbMcljWnNESIef.vOQUaOUqDmn0B9NZrd1JQPhna1QIW", Role = UserRole.Patient },
                new User { Id = 4, FullName = "Dr. John Smith", Email = "john@hospital.com", PasswordHash = "$2a$12$ovyrpA80WJR5Kd9s8TpsQOUdZpD3aof9vEI..6pCkg1c7YKHPZAWG", Role = UserRole.Doctor },
                new User { Id = 5, FullName = "Dr. Sarah Lee", Email = "sarah@hospital.com", PasswordHash = "$2a$12$ovyrpA80WJR5Kd9s8TpsQOUdZpD3aof9vEI..6pCkg1c7YKHPZAWG", Role = UserRole.Doctor },
                new User { Id = 6, FullName = "Dr. Mike Tyson", Email = "mike@hospital.com", PasswordHash = "$2a$12$ovyrpA80WJR5Kd9s8TpsQOUdZpD3aof9vEI..6pCkg1c7YKHPZAWG", Role = UserRole.Doctor },
                new User { Id = 7, FullName = "Dr. Emily Davis", Email = "emily@hospital.com", PasswordHash = "$2a$12$ovyrpA80WJR5Kd9s8TpsQOUdZpD3aof9vEI..6pCkg1c7YKHPZAWG", Role = UserRole.Doctor }
            );


            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { Id = 1, UserId = 4, SpecialtyId = 6, Name = "Dr. John Smith", Mode = ConsultationMode.Online, ConsultationFee = 500m },
                new Doctor { Id = 2, UserId = 5, SpecialtyId = 3, Name = "Dr. Sarah Lee", Mode = ConsultationMode.Offline, ConsultationFee = 800m },
                new Doctor { Id = 3, UserId = 6, SpecialtyId = 7, Name = "Dr. Mike Tyson", Mode = ConsultationMode.Online, ConsultationFee = 600m },
                new Doctor { Id = 4, UserId = 7, SpecialtyId = 5, Name = "Dr. Emily Davis", Mode = ConsultationMode.Offline, ConsultationFee = 1000m }
            );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment { Id = 1, PatientId = 2, DoctorId = 1, AppointmentDate = new DateTime(2026, 3, 31), TimeSlot = TimeSlot.SLOT_09AM_TO_10AM, Status = AppointmentStatus.Booked, TotalAmount = 500m, ModeArtifact = "https://meet.hcl.com/room-john" },

                new Appointment { Id = 2, PatientId = 3, DoctorId = 2, AppointmentDate = new DateTime(2026, 3, 31), TimeSlot = TimeSlot.SLOT_10AM_TO_11AM, Status = AppointmentStatus.Booked, TotalAmount = 800m, ModeArtifact = "Apollo Clinic, Room 101" },

                new Appointment { Id = 3, PatientId = 2, DoctorId = 3, AppointmentDate = new DateTime(2026, 3, 29), TimeSlot = TimeSlot.SLOT_11AM_TO_12PM, Status = AppointmentStatus.Completed, TotalAmount = 600m, ModeArtifact = "https://meet.hcl.com/room-mike" },

                new Appointment { Id = 4, PatientId = 3, DoctorId = 4, AppointmentDate = new DateTime(2026, 3, 29), TimeSlot = TimeSlot.SLOT_02PM_TO_03PM, Status = AppointmentStatus.NoShow, TotalAmount = 1000m, ModeArtifact = "Apollo Clinic, Room 102" },

                new Appointment { Id = 5, PatientId = 2, DoctorId = 1, AppointmentDate = new DateTime(2026, 4, 1), TimeSlot = TimeSlot.SLOT_03PM_TO_04PM, Status = AppointmentStatus.Cancelled, TotalAmount = 500m, ModeArtifact = "https://meet.hcl.com/room-john" }
            );

        }
    }
}
