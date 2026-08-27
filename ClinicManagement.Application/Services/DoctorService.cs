using ClinicManagement.Application.Common;
using ClinicManagement.Application.Common.Result;
using ClinicManagement.Application.Contracts.Repositories;
using ClinicManagement.Application.Contracts.Services;
using ClinicManagement.Application.Dtos;
using ClinicManagement.Application.Entities;
using ClinicManagement.Application.Mapping;
using ClinicManagement.Application.Specifications;
using ClinicManagement.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Services
{
    public class DoctorService(IUnitOfWork unitOfWork) : IDoctorService
    {
        private readonly IGenericRepository<Doctor, int> _doctorRepo = unitOfWork.GetRepository<Doctor, int>();


        public async Task<Result<PaginationResult<DoctorDto>>> GetAllDoctorsAsync(DoctorQueryParams query, CancellationToken ct = default)
        {
            DoctorSpecification specs = new(query);
            var result = await _doctorRepo.GetAllAsync(specs, ct);
            //var countSpecs = new DoctorSpecification(query); // true for count only
            var count = await _doctorRepo.CountAsync(specs, ct);

            List<DoctorDto> resultDto = [.. result.Select(d => d.ToDto())];


            return Result<PaginationResult<DoctorDto>>.Ok(new PaginationResult<DoctorDto>(resultDto, count, query.PageSize, query.PageNumber)); // [] if result = null


        }

        public async Task<Result<DoctorDto>> GetDoctorByIdAsync(int doctorId, CancellationToken ct = default)
        {
            var result = await _doctorRepo.GetByIdAsync(new DoctorByIdSpecification(doctorId), ct);

            if (result == null)
                return Result<DoctorDto>.Failure(Error.NotFound("Doctor not found", "Doctor.NotFound", $"Can't find doctor with id:{doctorId}")); // Not Found => return Result.NotFound();


            var doctorDto = result.ToDto();
            if (doctorDto == null) return Result<DoctorDto>.Failure(Error.Conflict());
            // still should map the result to doctorDto here either using AutoMapper or manually mapping it to the DoctorDto
            return Result<DoctorDto>.Ok(doctorDto);
        }
        public async Task<Result<DoctorDto>> CreateDoctorAsync(CreateDoctorDto newDoctorDto, CancellationToken ct = default)
        {
            // Check if the Department exists
            var isDepartmentExsists = await unitOfWork.GetRepository<Department, int>().GetByIdAsync(new BaseSpecification<Department, int>(d => d.Id == newDoctorDto.DepartmentId), ct) != null;

            if (!isDepartmentExsists)
                /*Maybe with the result pattern we can handle this with a proper way*/
                return Result<DoctorDto>.Failure(Error.Validation("Department doesn't exists", "Deparment.NotFound", $"Can't add doctor with department id:{newDoctorDto.DepartmentId}"));

            var doctor = newDoctorDto.ToDoctor();
            if (doctor == null) return Result<DoctorDto>.Failure(Error.Conflict());
            await _doctorRepo.AddAsync(doctor, ct);

            var result = await unitOfWork.SaveChangesAsync(ct);

            if (result > 0)
                return Result<DoctorDto>.Ok(doctor.ToDto());
            // Save changes failed => Doctor Field to add
            return Result<DoctorDto>.Failure(Error.Failure()); // this would handle better with the result pattern
        }

        public async Task<Result> DeleteDoctorAsync(int id, CancellationToken ct = default)
        {
            var doctor = await _doctorRepo.GetByIdAsync(id, ct);
            if (doctor is null) return Result.Failure(Error.NotFound("Doctor not found", "Doctor.NotFound", $"Can't find doctor with id:{id}"));

            _doctorRepo.Delete(doctor);
            var changes = await unitOfWork.SaveChangesAsync(ct);
            return changes > 0 ? Result.Ok() : Result.Failure(Error.Failure());
        }

        public async Task<Result<DoctorDto>> UpdateDoctorAsync(int doctorId, UpdateDoctorDto newDoctorData, CancellationToken ct = default)
        {
            var specs = new DoctorByIdSpecification(doctorId);
            var doctor = await _doctorRepo.GetByIdAsync(specs, ct);
            //var doctor = await _doctorRepo.GetByIdAsync(doctorId, ct);
            if (doctor is null) return Result<DoctorDto>.Failure(Error.NotFound("Doctor Not Found", "Doctor.NotFound", $"Can't find doctor with id:{doctorId}"));

            doctor.PhoneNumber = newDoctorData.PhoneNumber;
            doctor.Salary = newDoctorData.Salary;
            doctor.YearsOfExperience = newDoctorData.YearsOfExperience;
            doctor.Email = newDoctorData.Email;

            _doctorRepo.Update(doctor);

            var result = await unitOfWork.SaveChangesAsync(ct);

            var updatedDoctorDto = doctor.ToDto();


            return result > 0 ? Result<DoctorDto>.Ok(updatedDoctorDto) : Result<DoctorDto>.Failure(Error.Failure());
            
        }
    }
}
