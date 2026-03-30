using DoctorAppointmentSystem.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DoctorAppointmentSystem.Shared.DTOs.Request
{
    public class BookAppointmentRequestDto
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public TimeSlot TimeSlot { get; set; }
    }
}
