using ClinicManagement.Application.Common;
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


        public async Task<PaginationResult<DoctorDto>> GetAllDoctorsAsync(DoctorQueryParams query,CancellationToken ct = default)
        {
            DoctorSpecification specs = new(query);
            var result = await _doctorRepo.GetAllAsync(specs,ct);
            //var countSpecs = new DoctorSpecification(query); // true for count only
            var count = await _doctorRepo.CountAsync(specs,ct);

            List<DoctorDto> resultDto = [.. result.Select(d => d.ToDto())];


            return new PaginationResult<DoctorDto>(resultDto, count, query.PageSize, query.PageNumber); // [] if result = null


        }

        public async Task<DoctorDto?> GetDoctorByIdAsync(int doctorId,CancellationToken ct = default)
        {
            var result = await _doctorRepo.GetByIdAsync(new DoctorByIdSpecification(doctorId),ct);

            if (result == null)
                return null; // Not Found => return Result.NotFound();


            var doctorDto = result.ToDto();
            // still should map the result to doctorDto here either using AutoMapper or manually mapping it to the DoctorDto
            return doctorDto;
        }
        public async Task<DoctorDto> CreateDoctorAsync(CreateDoctorDto newDoctorDto, CancellationToken ct = default)
        {
            // Check if the Department exists
            var isDepartmentExsists = await unitOfWork.GetRepository<Department, int>().GetByIdAsync(new BaseSpecification<Department, int>(d => d.Id == newDoctorDto.DepartmentId), ct) != null;

            if (!isDepartmentExsists)
                /*Maybe with the result pattern we can handle this with a proper way*/
                throw new ArgumentException($"Department with ID {newDoctorDto.DepartmentId} does not exist.");


            var doctor = newDoctorDto.ToDoctor();

            await _doctorRepo.AddAsync(doctor, ct);

            var result = await unitOfWork.SaveChangesAsync(ct);

            if (result > 0)
                return doctor.ToDto();
            // Save changes failed => Doctor Field to add
            throw new Exception("Failed to add new doctor"); // this would handle better with the result pattern
        }
    }
}
