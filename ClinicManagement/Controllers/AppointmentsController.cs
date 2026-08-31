using ClinicManagement.API.Controllers.Base;
using ClinicManagement.Application.Contracts.Services;
using ClinicManagement.Application.Dtos.Appointment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController(IAppointmentService appointmentService) : APIBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAppointmentsForPatient([FromQuery] AppointmentQueryParams queryParams, CancellationToken ct = default)
        {
            var allAppointmentsResult = await appointmentService.GetAllAppointmentsAsync(queryParams, ct);
            return ToActionResult(allAppointmentsResult);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(Guid id, CancellationToken ct = default)
        {
            var appointment = await appointmentService.GetAppointmentByIdAsync(id, ct);
            return ToActionResult(appointment);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto createAppointmentDto, CancellationToken ct = default)
        {
            var result = await appointmentService.CreateAppointmentAsync(createAppointmentDto, ct);
            if (result.IsFailure) return ToProblem(result);

            return CreatedAtAction(nameof(GetAppointmentById), new { id = result.Data!.Id }, result.Data);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(Guid id, [FromBody] UpdateAppointmentDto updateAppointmentData, CancellationToken ct = default)
        {
            var result = await appointmentService.UpdateAppointmentAsync(id, updateAppointmentData, ct);
            return ToActionResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id, CancellationToken ct = default)
        {
            var result = await appointmentService.DeleteAppointmentAsync(id, ct);

            return ToActionResult(result);

        }
    }
}
