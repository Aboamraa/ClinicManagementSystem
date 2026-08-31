using ClinicManagement.Application.Common;
using ClinicManagement.Application.Common.Result;
using ClinicManagement.Application.Contracts.Repositories;
using ClinicManagement.Application.Contracts.Services;
using ClinicManagement.Application.Dtos.Appointment;
using ClinicManagement.Application.Entities;
using ClinicManagement.Application.Entities.Enums;
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
    public class AppointmentService(IUnitOfWork unitOfWork) : IAppointmentService
    {
        private readonly IGenericRepository<Appointment, Guid> _appointmentRepo = unitOfWork.GetRepository<Appointment, Guid>();
        public async Task<Result<PaginationResult<AppointmentDto>>> GetAllAppointmentsAsync(AppointmentQueryParams queryParams, CancellationToken ct = default)
        {

            var specs = new AppointmentSpecification(queryParams);
            var allAppointments = await _appointmentRepo.GetAllAsync(specs, ct);
            var count = await _appointmentRepo.CountAsync(specs, ct);

            var allAppointmentsDto = allAppointments.Select(a => a.ToDto());
            var paginationResult = new PaginationResult<AppointmentDto>([.. allAppointmentsDto], count, queryParams.PageSize, queryParams.PageNumber);

            return Result<PaginationResult<AppointmentDto>>.Ok(paginationResult);
        }

        public async Task<Result<AppointmentDto>> GetAppointmentByIdAsync(Guid id, CancellationToken ct = default)
        {

            var appointment = await _appointmentRepo.GetByIdAsync(id, ct);
            if (appointment == null) return Result<AppointmentDto>.Failure(Error.NotFound("Appointment Not Found", "Appointment.NotFound", $"Can't find Appointment with ID:{id}"));

            var appointmentDto = appointment.ToDto();

            return Result<AppointmentDto>.Ok(appointmentDto);
        }
        public async Task<Result<AppointmentDto>> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto, CancellationToken ct = default)
        {
            // validate the date
            if (createAppointmentDto.StartTime == default) return Result<AppointmentDto>.Failure(Error.Validation("Start time invalid", "Appointment.ValidationError", $"Start time is required"));
            //if (createAppointmentDto.StartTime >= createAppointmentDto.EndTime) return Result<AppointmentDto>.Failure(Error.Validation("Start time invalid", "Appointment.ValidationError", $"Start must be older than the end time"));
            if (createAppointmentDto.EndTime <= createAppointmentDto.StartTime || createAppointmentDto.StartTime <= DateTime.Now) return Result<AppointmentDto>.Failure(Error.Validation("Start or end time are invalid", "Appointment.ValidationError", $"Start and end time must be in the future"));

            // Check doctor exists
            var isDocotrExists = await unitOfWork.GetRepository<Doctor, int>().IsExistsAsync(createAppointmentDto.DoctorId, ct);
            if (!isDocotrExists) return Result<AppointmentDto>.Failure(Error.NotFound("Doctor not found", "Doctor.NotFound", $"Can't find doctor with id:{createAppointmentDto.DoctorId}"));

            // Check patient exists
            var isPatientExists = await unitOfWork.GetRepository<Doctor, int>().IsExistsAsync(createAppointmentDto.DoctorId, ct);
            if (!isPatientExists) return Result<AppointmentDto>.Failure(Error.NotFound("Patient not found", "Patient.NotFound", $"Can't find patient with id:{createAppointmentDto.PatientId}"));

            // check if doctor available
            var appointmentSpecs = new AppointmentSpecification(new AppointmentQueryParams() { WithDoctor = createAppointmentDto.DoctorId, BeforeDate = createAppointmentDto.EndTime, AfterDate = createAppointmentDto.StartTime });
            var isNotAvailable = await _appointmentRepo.IsExistsAsync(appointmentSpecs, ct);
            if (isNotAvailable) return Result<AppointmentDto>.Failure(Error.Conflict("can't create appointment","Appointment.Conflict","The doctor is not available"));

            var newAppointment = new Appointment()
            {
                Id = Guid.NewGuid(),
                StartTime = createAppointmentDto.StartTime,
                EndTime = createAppointmentDto.EndTime,
                Price = createAppointmentDto.Price,
                DoctorId = createAppointmentDto.DoctorId,
                PatientId = createAppointmentDto.PatientId,
                AppointmentStatus = AppointmentStatus.Scheduled

            };
            await _appointmentRepo.AddAsync(newAppointment, ct);
            var result = await unitOfWork.SaveChangesAsync(ct);

            var newAppointmentDto = newAppointment.ToDto();
            return result > 0 ? Result<AppointmentDto>.Ok(newAppointmentDto) : Result<AppointmentDto>.Failure(Error.Failure());

        }



        public async Task<Result<AppointmentDto>> UpdateAppointmentAsync(Guid appointmentId, UpdateAppointmentDto newAppointmentData, CancellationToken ct = default)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(appointmentId, ct);
            if (appointment == null) return Result<AppointmentDto>.Failure(Error.NotFound("Appointment Not Found", "Appointment.NotFound", $"Can't find Appointment with ID:{appointmentId}"));

            if (newAppointmentData.StartTime >= newAppointmentData.EndTime) return Result<AppointmentDto>.Failure(Error.Validation("Invalid start/End Time", "Appointment.ValidationError", "Invalid Start or End Time"));
            if (newAppointmentData.StartTime == default || newAppointmentData.EndTime == default) return Result<AppointmentDto>.Failure(Error.Validation("Invalid start/end time", "Appointment.ValidationError", "Start and End times are required"));

            appointment.StartTime = newAppointmentData.StartTime;
            appointment.EndTime = newAppointmentData.EndTime;
            appointment.Price = newAppointmentData.Price;

            _appointmentRepo.Update(appointment);
            var result = await unitOfWork.SaveChangesAsync(ct);
            var updatedAppointmentDto = appointment.ToDto();

            return result > 0 ? Result<AppointmentDto>.Ok(updatedAppointmentDto) : Result<AppointmentDto>.Failure(Error.Failure());
        }
        public async Task<Result> DeleteAppointmentAsync(Guid appointmentId, CancellationToken ct = default)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(appointmentId, ct);
            if (appointment == null) return Result.Failure(Error.NotFound("Appointment Not Found", "Appointment.NotFound", $"Can't find Appointment with ID:{appointmentId}"));
            if (appointment.StartTime <= DateTime.Now.AddHours(2)) return Result.Failure(Error.Validation("Appointment can't be deleted", "Delete.ValidationError", "Can't delete appointment before 2 hours of starting or already started/Ended"));

            appointment.AppointmentStatus = AppointmentStatus.Cancelled;

            //_appointmentRepo.Delete(appointment);

            var result = await unitOfWork.SaveChangesAsync(ct);
            return result > 0 ? Result.Ok() : Result.Failure(Error.Failure());

        }
    }
}
