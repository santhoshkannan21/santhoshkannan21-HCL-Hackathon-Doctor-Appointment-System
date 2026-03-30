using DoctorAppointmentSystem.API.Services.Interfaces;
using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DoctorAppointmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentsController(IAppointmentService appointmentService) => _appointmentService = appointmentService;

        [HttpPost("book")]
        public async Task<ActionResult<AppointmentResponseDto>> Book(BookAppointmentRequestDto request)
        {
            var patientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            return Ok(await _appointmentService.BookAppointmentAsync(request, patientId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentResponseDto>> GetById(int id)
            => Ok(await _appointmentService.GetAppointmentByIdAsync(id));

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult<AppointmentResponseDto>> UpdateStatus(int id, UpdateStatusRequestDto request)
            => Ok(await _appointmentService.UpdateStatusAsync(id, request));

        [HttpPut("{id}/cancel")]
        public async Task<ActionResult<AppointmentResponseDto>> Cancel(int id)
        {
            var patientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            return Ok(await _appointmentService.CancelAppointmentAsync(id, patientId));
        }
    }

}