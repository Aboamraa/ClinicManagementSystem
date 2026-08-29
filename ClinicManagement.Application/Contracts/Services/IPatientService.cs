using ClinicManagement.Application.Common;
using ClinicManagement.Application.Common.Result;
using ClinicManagement.Application.Dtos.Patient;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Contracts.Services
{
    public interface IPatientService
    {

        Task<Result<PaginationResult<PatientDto>>> GetAllPatientsAsync(PatientQueryParams queryParams, CancellationToken ct = default);
        Task<Result<PatientDto>> GetPatientByIdAsync(Guid id, CancellationToken ct = default);

        Task<Result<PatientDto>> CreatePatientAsync(CreatePatientDto newPatient, CancellationToken ct = default);
        Task<Result<PatientDto>> UpdatePatientAsync(Guid PatientToUpdateId, UpdatePatientDto updatePatientData, CancellationToken ct = default);
        Task<Result> DeletePatientAsync(Guid id, CancellationToken ct = default);


    }
}
