using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Dtos.Patient
{
    public class UpdatePatientDto
    {
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; } = default!;
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Weight is required")]
        [Range(10, 300, ErrorMessage = "Please enter valid weight value")]
        public double Weight { get; set; }
        [Required(ErrorMessage = "Height is required")]
        [Range(30, 300, ErrorMessage = "Please enter valid height value")]
        public double Height { get; set; }
    }
}
