using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Shared.DTOs.Response
{
    public class DailySummaryResponseDto
    {
        public DateTime SummaryDate { get; set; }
        public int TotalAppointments { get; set; }
        public decimal TotalRevenue { get; set; }
        public int OnlineCount { get; set; }
        public int OfflineCount { get; set; }
    }
}
