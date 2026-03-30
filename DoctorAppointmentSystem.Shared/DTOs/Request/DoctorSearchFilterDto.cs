using DoctorAppointmentSystem.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Shared.DTOs.Request
{
    public class DoctorSearchFilterDto
    {
        public ConsultationMode Mode { get; set; }
        public int SpecialtyId { get; set; }
    }
}
