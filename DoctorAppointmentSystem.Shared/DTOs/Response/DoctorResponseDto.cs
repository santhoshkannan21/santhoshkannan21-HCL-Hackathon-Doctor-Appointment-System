using DoctorAppointmentSystem.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Shared.DTOs.Response
{
    public class DoctorResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string SpecialtyName { get; set; } = string.Empty;
        public string Mode { get; set; } = string.Empty;
        public decimal ConsultationFee { get; set; }
    }
}
