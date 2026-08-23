using ClinicManagement.Application.Common;
using ClinicManagement.Application.Dtos;
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
        Task<PaginationResult<DoctorDto>> GetAllDoctorsAsync(DoctorQueryParams query, CancellationToken ct = default);

        Task<DoctorDto?> GetDoctorByIdAsync(int doctorId, CancellationToken ct = default);
        Task<DoctorDto> CreateDoctorAsync(CreateDoctorDto newDoctorDto, CancellationToken ct = default);
    }
}
