using ClinicManagement.Application.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Dtos.Appointment
{
    public class CreateAppointmentDto
    {
        [Required(ErrorMessage = "Start time required")]
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0, 999999, ErrorMessage = "Price range [0,99999] please enter valid price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Doctor Id is required")]
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "Patient Id is required")]
        public Guid PatientId { get; set; }
    }
}
