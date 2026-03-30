using DoctorAppointmentSystem.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DoctorAppointmentSystem.Shared.DTOs.Request
{
    public class UpdateStatusRequestDto
    {
        [Required]
        public AppointmentStatus NewStatus { get; set; }
    }
}
