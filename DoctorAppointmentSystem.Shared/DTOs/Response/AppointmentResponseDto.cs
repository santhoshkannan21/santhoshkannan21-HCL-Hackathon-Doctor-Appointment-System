using DoctorAppointmentSystem.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Shared.DTOs.Response
{
    public class AppointmentResponseDto
    {
        public int Id { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string SpecialtyName { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;

        public DateTime AppointmentDate { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public AppointmentStatus Status { get; set; }

        public string ModeArtifact { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
    }
}
