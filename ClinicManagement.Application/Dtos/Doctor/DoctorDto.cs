using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Dtos.Doctor
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;

        public int YearsOfExperience { get; set; }
        public decimal Salary { get; set; }
        public string DepartmentTitle { get; set; } = default!;

    }
}
