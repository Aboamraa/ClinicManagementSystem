using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Dtos.Patient
{
    public class CreatePatientDto
    {
        [Required(ErrorMessage = "Name is required")]
        [Length(2, 50, ErrorMessage = "Name length can't be less than 2 charters or longer 50 characters")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; } = default!;
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "Birth date is required")]
        public DateOnly BirthDate { get; set; } = default!;
        [Required(ErrorMessage = "Weight is required")]
        [Range(10, 300, ErrorMessage = "Please enter valid weight value")]
        public double Weight { get; set; }
        [Required(ErrorMessage = "Height is required")]
        [Range(30, 300, ErrorMessage = "Please enter valid height value")]
        public double Height { get; set; }
    }
}
