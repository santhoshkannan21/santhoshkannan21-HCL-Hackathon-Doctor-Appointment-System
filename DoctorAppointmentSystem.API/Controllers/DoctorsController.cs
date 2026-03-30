using DoctorAppointmentSystem.API.Services.Interfaces;
using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;
using DoctorAppointmentSystem.Shared.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorsController(IDoctorService doctorService) => _doctorService = doctorService;

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<DoctorResponseDto>>> Search([FromQuery] DoctorSearchFilterDto filter)
            => Ok(await _doctorService.SearchDoctorsAsync(filter));

        [HttpGet("available-slots")]
        public async Task<ActionResult<IEnumerable<TimeSlot>>> GetSlots([FromQuery] GetAvailableSlotRequestDto request)
            => Ok(await _doctorService.GetAvailableSlotsAsync(request));
    }
}
