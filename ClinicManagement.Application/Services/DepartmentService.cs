using ClinicManagement.Application.Common;
using ClinicManagement.Application.Common.Result;
using ClinicManagement.Application.Contracts.Repositories;
using ClinicManagement.Application.Contracts.Services;
using ClinicManagement.Application.Dtos.Department;
using ClinicManagement.Application.Dtos.Doctor;
using ClinicManagement.Application.Mapping;
using ClinicManagement.Application.Specifications;
using ClinicManagement.Domain.Contracts;
using ClinicManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Services
{
    public class DepartmentService(IUnitOfWork unitOfWork) : IDepartmentService
    {
        private readonly IGenericRepository<Department, int> _departmentRepo = unitOfWork.GetRepository<Department, int>();
        public async Task<Result<PaginationResult<DepartmentDto>>> GetAllDepartmentsAsync(DepartmentQueryParams queryParams, CancellationToken ct = default)
        {
            var specs = new DepartmentSpecification(queryParams);
            var allDepartments = await _departmentRepo.GetAllAsync(specs, ct);
            var allDepartmentsDto = allDepartments.Select(d => d.ToDto());

            var paginationResult = new PaginationResult<DepartmentDto>([.. allDepartmentsDto], allDepartments.Count, queryParams.PageSize, queryParams.PageNumber);

            return Result<PaginationResult<DepartmentDto>>.Ok(paginationResult);
        }

        public async Task<Result<DepartmentDto>> GetDepartmentByIdAsync(int id, CancellationToken ct = default)
        {
            var department = await _departmentRepo.GetByIdAsync(id, ct);

            if (department == null) return Result<DepartmentDto>.Failure(Error.NotFound("Department not found", "Department.NotFound", $"can't find department with id:{id}"));

            var departmentDto = department.ToDto();

            return Result<DepartmentDto>.Ok(departmentDto);
        }
        public async Task<Result<DepartmentDto>> CreateDepartmentAsync(CreateDepartmentDto newDepartmentDto, CancellationToken ct = default)
        {
            DepartmentByNameSpecification specs = new DepartmentByNameSpecification(newDepartmentDto.Name);
            //var departmentWithSameName = await _departmentRepo.GetAllAsync(specs, ct);
            var departmentWithSameName = await _departmentRepo.IsExistsAsync(specs, ct);

            if (departmentWithSameName) return Result<DepartmentDto>.Failure(Error.Conflict("Department name conflict", "Department.Conflict", $"Department with name {newDepartmentDto.Name} already exists"));

            var newDepartment = newDepartmentDto.ToDepartment();
            await _departmentRepo.AddAsync(newDepartment, ct);

            var result = await unitOfWork.SaveChangesAsync(ct);
            var departmentDto = newDepartment.ToDto();

            return result > 0 ? Result<DepartmentDto>.Ok(departmentDto) : Result<DepartmentDto>.Failure(Error.Failure());
        }
        public async Task<Result<DepartmentDto>> UpdateDepartmentAsync(int departmentToUpdateId, UpdateDepartmentDto departmentDataToUpdate, CancellationToken ct = default)
        {
            var departmentToUpdate = await _departmentRepo.GetByIdAsync(departmentToUpdateId, ct);
            if (departmentToUpdate is null) return Result<DepartmentDto>.Failure(Error.NotFound("Department not found", "Department.NotFound", $"can't find department with id:{departmentToUpdateId}"));
            if (departmentToUpdate.Name != departmentDataToUpdate.Name)
            {
                DepartmentByNameSpecification specs = new DepartmentByNameSpecification(departmentDataToUpdate.Name);
                var departmentWithSameName = await _departmentRepo.GetAllAsync(specs, ct);
                if (departmentWithSameName.Count > 0) return Result<DepartmentDto>.Failure(Error.Conflict("Department name conflict", "Department.Conflict", $"Department with name {departmentDataToUpdate.Name} already exists"));
            }

            departmentToUpdate.Name = departmentDataToUpdate.Name;
            departmentToUpdate.Description = departmentDataToUpdate.Description;

            _departmentRepo.Update(departmentToUpdate);

            var result = await unitOfWork.SaveChangesAsync(ct);

            var departmentDto = departmentToUpdate.ToDto();

            return result > 0 ? Result<DepartmentDto>.Ok(departmentDto) : Result<DepartmentDto>.Failure(Error.Failure());

        }
        // what happen to doctors in this department that will be deleted?
        public async Task<Result> DeleteDepartmentAsync(int id, CancellationToken ct = default)
        {
            var department = await _departmentRepo.GetByIdAsync(id, ct);
            if (department is null) return Result.Failure(Error.NotFound("Department not found", "Department.NotFound", $"can't find department with id:{id}"));

            //Check if is there any doctor in this department (yes => cant delete, No => safe to delete)
            var doctorsInDepartmentSpecs = new DoctorSpecification(new DoctorQueryParams() { DepartmentId = id });
            //var doctorsInDepartment = await unitOfWork.GetRepository<Doctor, int>().GetAllAsync(doctorsInDepartmentSpecs, ct);
            var doctorsInDepartment = await unitOfWork.GetRepository<Doctor, int>().IsExistsAsync(doctorsInDepartmentSpecs, ct);



            if (doctorsInDepartment) return Result.Failure(Error.Conflict("Department has doctors", "DeleteDepartment.Conflict", "department has doctors, please delete the doctors first"));

            _departmentRepo.Delete(department);

            var res = await unitOfWork.SaveChangesAsync(ct);

            return res > 0 ? Result.Ok() : Result.Failure(Error.Failure());
        }


    }
}
