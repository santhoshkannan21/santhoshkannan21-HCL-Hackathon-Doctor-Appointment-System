using DoctorAppointmentSystem.API.Services.Interfaces;
using DoctorAppointmentSystem.Shared.DTOs.Request;
using DoctorAppointmentSystem.Shared.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] 
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService) => _adminService = adminService;

        [HttpGet("daily-summary")]
        public async Task<ActionResult<DailySummaryResponseDto>> GetSummary([FromQuery] DailySummaryFilterDto filter)
            => Ok(await _adminService.GetDailySummaryAsync(filter));
    }
}
