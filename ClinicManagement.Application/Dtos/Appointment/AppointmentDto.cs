using ClinicManagement.Application.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Dtos.Appointment
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public decimal Price { get; set; }


        public int DoctorId { get; set; }


        public Guid PatientId { get; set; }
    }
}
