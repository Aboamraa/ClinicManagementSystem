using ClinicManagement.Application.Common;
using ClinicManagement.Application.Common.Result;
using ClinicManagement.Application.Dtos.Doctor;
using ClinicManagement.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Contracts.Services
{
    public interface IDoctorService
    {
        Task<Result<PaginationResult<DoctorDto>>> GetAllDoctorsAsync(DoctorQueryParams query, CancellationToken ct = default);

        Task<Result<DoctorDto>> GetDoctorByIdAsync(int doctorId, CancellationToken ct = default);
        Task<Result<DoctorDto>> CreateDoctorAsync(CreateDoctorDto newDoctorDto, CancellationToken ct = default);
        Task<Result> DeleteDoctorAsync(int id, CancellationToken ct = default);

        Task<Result<DoctorDto>> UpdateDoctorAsync(int doctorId, UpdateDoctorDto newDoctorData, CancellationToken ct = default);

    }
}
