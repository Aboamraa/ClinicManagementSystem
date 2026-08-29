using ClinicManagement.Application.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Dtos.Doctor
{
    public class UpdateDoctorDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter valid Email address")]
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        [Required(ErrorMessage = "Please enter your years of experience")]
        [Range(0, 100, ErrorMessage = "Please enter a valid years of experience (0,100)")]
        public int YearsOfExperience { get; set; }

        [Range(0, 999999, ErrorMessage = "Please enter a valid salary value")]
        public decimal Salary { get; set; }
    }
}
