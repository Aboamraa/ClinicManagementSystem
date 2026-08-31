using ClinicManagement.Application.Common;
using ClinicManagement.Application.Common.Result;
using ClinicManagement.Application.Dtos.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Contracts.Services
{
    public interface IDepartmentService
    {
        Task<Result<PaginationResult<DepartmentDto>>> GetAllDepartmentsAsync(DepartmentQueryParams queryParams, CancellationToken ct = default);

        Task<Result<DepartmentDto>> GetDepartmentByIdAsync(int id, CancellationToken ct = default);
        Task<Result<DepartmentDto>> CreateDepartmentAsync(CreateDepartmentDto newDepartmentDto, CancellationToken ct = default);

        Task<Result<DepartmentDto>> UpdateDepartmentAsync(int departmentToUpdateId, UpdateDepartmentDto departmentDataToUpdate, CancellationToken ct = default);

        Task<Result> DeleteDepartmentAsync(int id, CancellationToken ct = default);
    }
}
