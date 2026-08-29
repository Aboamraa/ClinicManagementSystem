using ClinicManagement.Application.Common;
using ClinicManagement.Application.Common.Result;
using ClinicManagement.Application.Contracts.Repositories;
using ClinicManagement.Application.Contracts.Services;
using ClinicManagement.Application.Dtos.Patient;
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
    public class PatientService(IUnitOfWork unitOfWork) : IPatientService
    {
        private readonly IGenericRepository<Patient, Guid> PatientRepo = unitOfWork.GetRepository<Patient, Guid>();

        public async Task<Result<PaginationResult<PatientDto>>> GetAllPatientsAsync(PatientQueryParams queryParams, CancellationToken ct = default)
        {
            var specs = new PatientSpecification(queryParams);
            var allPatientsResult = await PatientRepo.GetAllAsync(specs, ct);

            var allPatientsCount = await PatientRepo.CountAsync(specs, ct);
            var allPatientsDto = new List<PatientDto>(allPatientsResult.Count);

            foreach (var patient in allPatientsResult)
                allPatientsDto.Add(patient.ToDto());


            return Result<PaginationResult<PatientDto>>.Ok(new PaginationResult<PatientDto>(allPatientsDto, allPatientsCount, queryParams.PageSize, queryParams.PageNumber));
        }

        public async Task<Result<PatientDto>> GetPatientByIdAsync(Guid id, CancellationToken ct = default)
        {
            var patient = await PatientRepo.GetByIdAsync(id, ct);
            if (patient is null) return Result<PatientDto>.Failure(Error.NotFound("Patient Not Found", "Patient.NotFound", $"Can't find patient with id:{id}"));

            var patientDto = patient.ToDto();
            return Result<PatientDto>.Ok(patientDto);
        }

        public async Task<Result<PatientDto>> CreatePatientAsync(CreatePatientDto newPatient, CancellationToken ct = default)
        {
            var patientSpecs = new PatientByInfoSpecification(newPatient.PhoneNumber, newPatient.Email);
            var isRepetedPatinetData = await PatientRepo.IsExistsAsync(patientSpecs, ct);

            if (isRepetedPatinetData) return Result<PatientDto>.Failure(Error.Conflict("Data already exists", "Patient.Conflict", "Email or Phone number already exists"));


            var patient = new Patient()
            {
                Id = Guid.NewGuid(),
                Name = newPatient.Name,
                Email = newPatient.Email,
                PhoneNumber = newPatient.PhoneNumber,
                Height = newPatient.Height,
                Weight = newPatient.Weight,
                BirthDate = newPatient.BirthDate
            };
            await PatientRepo.AddAsync(patient, ct);

            var added = await unitOfWork.SaveChangesAsync(ct);

            var patientDto = patient.ToDto();

            return added > 0 ? Result<PatientDto>.Ok(patientDto) : Result<PatientDto>.Failure(Error.Failure());
        }

        public async Task<Result<PatientDto>> UpdatePatientAsync(Guid PatientToUpdateId, UpdatePatientDto updatePatientData, CancellationToken ct = default)
        {
            // Make sure the patient exists:
            var patient = await PatientRepo.GetByIdAsync(PatientToUpdateId, ct);

            if (patient is null) return Result<PatientDto>.Failure(Error.NotFound("Patient not found", "Patient.NotFound", $"Can't find patient with id:{PatientToUpdateId}"));

            var patientSpecs = new PatientByInfoSpecification(updatePatientData.PhoneNumber, updatePatientData.Email);
            var isRepetedPatinetData = await PatientRepo.IsExistsAsync(patientSpecs, ct);

            if (isRepetedPatinetData) return Result<PatientDto>.Failure(Error.Conflict("Data already exists", "Patient.Conflict", "Email or Phone number already exists"));



            patient.PhoneNumber = updatePatientData.PhoneNumber;
            patient.Email = updatePatientData.Email;
            patient.Weight = updatePatientData.Weight;
            patient.Height = updatePatientData.Height;

            PatientRepo.Update(patient);
            var res = await unitOfWork.SaveChangesAsync(ct);

            var patientDto = patient.ToDto();

            return res > 0 ? Result<PatientDto>.Ok(patientDto) : Result<PatientDto>.Failure(Error.Failure());

        }
        public async Task<Result> DeletePatientAsync(Guid id, CancellationToken ct = default)
        {
            // Make sure the id exists
            var patient = await PatientRepo.GetByIdAsync(id, ct);

            if (patient is null) return Result.Failure(Error.NotFound("Patient not found", "Patient.NotFound", $"Can't find patient with id:{id}"));

            PatientRepo.Delete(patient);

            var result = await unitOfWork.SaveChangesAsync(ct);
            return result > 0 ? Result.Ok() : Result.Failure(Error.Failure());
        }

    }
}
