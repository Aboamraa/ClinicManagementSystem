using ClinicManagement.API.Controllers.Base;
using ClinicManagement.Application.Common;
using ClinicManagement.Application.Contracts.Services;
using ClinicManagement.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController(IDoctorService doctorService) : APIBaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(PaginationResult<DoctorDto>), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAllDoctors([FromQuery] DoctorQueryParams queryParams)
        {
            var result = await doctorService.GetAllDoctorsAsync(queryParams);
            return ToActionResult(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var result = await doctorService.GetDoctorByIdAsync(id);
            //if (result.IsFailure)
            //    return NotFound();

            //return Ok(result.Data);
            return ToActionResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto createDoctorDto, CancellationToken ct = default)
        {
            var result = await doctorService.CreateDoctorAsync(createDoctorDto, ct);
            if (result.IsFailure)
                return ToProblem(result);

            return CreatedAtAction(
                nameof(GetDoctorById),
                new { id = result.Data!.Id },
                result.Data
            );
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDoctor(int doctorId, CancellationToken ct = default)
        {
            var result = await doctorService.DeleteDoctorAsync(doctorId, ct);
            return ToActionResult(result);
        }
        [HttpPut("{doctorId}")]
        [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status200OK)]
        [ProducesResponseType( StatusCodes.Status400BadRequest)]
        [ProducesResponseType( StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDoctor(int doctorId, UpdateDoctorDto updateData, CancellationToken ct = default)
        {
            var result = await doctorService.UpdateDoctorAsync(doctorId, updateData, ct);
            return ToActionResult(result);
        }
    }
}
