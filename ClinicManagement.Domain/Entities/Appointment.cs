using ClinicManagement.Application.Entities.Abstract;
using ClinicManagement.Application.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Entities
{
    public class Appointment : BaseEntity<Guid>
    {
        // Id => inherited from BaseEntity => Guid
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public decimal Price { get; set; }


        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }


        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

    }
}
