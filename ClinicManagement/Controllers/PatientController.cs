using ClinicManagement.API.Controllers.Base;
using ClinicManagement.Application.Contracts.Services;
using ClinicManagement.Application.Dtos.Patient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController(IPatientService patientService) : APIBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPatientsAsync([FromQuery] PatientQueryParams patientQuery, CancellationToken ct)
        {
            var allPatientsResult = await patientService.GetAllPatientsAsync(patientQuery, ct);
            return ToActionResult(allPatientsResult);
        }
        [HttpGet("{patientId:guid}", Name = "GetPatientById")]
        public async Task<IActionResult> GetPatientById(Guid patientId, CancellationToken ct)
        {
            var patient = await patientService.GetPatientByIdAsync(patientId, ct);
            return ToActionResult(patient);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDto newPatient, CancellationToken ct)
        {
            var newPatientResult = await patientService.CreatePatientAsync(newPatient, ct);
            if (newPatientResult.IsSuccess)
                return CreatedAtAction(nameof(GetPatientById), "Patient", new { patientId = newPatientResult.Data!.Id }, newPatientResult.Data);


            return ToProblem(newPatientResult);
        }

        [HttpPut("{patientId:guid}")]
        public async Task<IActionResult> UpdatePatint(Guid patientId, [FromBody] UpdatePatientDto updatePatient, CancellationToken ct = default)
        {
            var res = await patientService.UpdatePatientAsync(patientId, updatePatient, ct);
            return ToActionResult(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePatient(Guid id, CancellationToken ct = default)
        {
            var result = await patientService.DeletePatientAsync(id, ct);

            return ToActionResult(result);
        }



    }
}
