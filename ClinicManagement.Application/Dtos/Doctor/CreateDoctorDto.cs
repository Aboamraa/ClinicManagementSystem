using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Dtos.Doctor
{
    public class CreateDoctorDto
    {
        [Required(ErrorMessage = "Name is required")]
        [Length(2, 100)]
        public string Name { get; set; } = default!;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter valid Email address")]
        public string Email { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;
        [Required(ErrorMessage = "Please enter your years of experience")]
        [Range(0, 100, ErrorMessage = "Please enter a valid years of experience (0,100)")]
        public int YearsOfExperience { get; set; }
        [Range(0, 999999, ErrorMessage = "Please enter a valid salary value")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "Please enter your department")]
        [Range(1,int.MaxValue,ErrorMessage ="Please enter valid department id")]
        public int DepartmentId { get; set; }
    }
}
