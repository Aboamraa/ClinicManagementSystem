using ClinicManagement.Application.Common;
using ClinicManagement.Application.Common.Result;
using ClinicManagement.Application.Dtos.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Contracts.Services
{
    public interface IAppointmentService
    {
        Task<Result<PaginationResult<AppointmentDto>>> GetAllAppointmentsAsync(AppointmentQueryParams queryParams, CancellationToken ct = default);
        Task<Result<AppointmentDto>> GetAppointmentByIdAsync(Guid id, CancellationToken ct = default);
        Task<Result<AppointmentDto>> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto, CancellationToken ct = default);
        Task<Result<AppointmentDto>> UpdateAppointmentAsync(Guid appointmentId, UpdateAppointmentDto newAppointmentData, CancellationToken ct = default);
        Task<Result> DeleteAppointmentAsync(Guid appointmentId, CancellationToken ct = default);
    }
}
