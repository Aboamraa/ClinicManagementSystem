using ClinicManagement.Application.Common;
using ClinicManagement.Application.Contracts.Services;
using ClinicManagement.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController(IDoctorService doctorService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginationResult<DoctorDto>>> GetAllDoctors([FromQuery] DoctorQueryParams queryParams)
        {
            var result = await doctorService.GetAllDoctorsAsync(queryParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetDoctorById(int id)
        {
            var result = await doctorService.GetDoctorByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DoctorDto>> CreateDoctor([FromBody] CreateDoctorDto createDoctorDto,CancellationToken ct = default)
        {
            var result = await doctorService.CreateDoctorAsync(createDoctorDto, ct);

            return CreatedAtAction(
                nameof(GetDoctorById),
                new { id = result.Id },
                result
            );
        }
    }
}
