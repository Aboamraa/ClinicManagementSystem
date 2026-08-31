using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Dtos.Appointment
{
    public class UpdateAppointmentDto
    {
        [Required(ErrorMessage = "Start time required")]
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0, 999999, ErrorMessage = "Price range [0,999999] please enter valid price")]
        public decimal Price { get; set; }


    }
}
