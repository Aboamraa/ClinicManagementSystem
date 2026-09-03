using ClinicManagement.Application.Dtos.Appointment;
using ClinicManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Mapping
{
    public static class AppointmentMapping
    {
        public static AppointmentDto ToDto(this Appointment appointment)

            => new AppointmentDto()
            {
                Id = appointment.Id,
                AppointmentStatus = appointment.AppointmentStatus,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                Price = appointment.Price

            };
    }
}
