using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Dtos.Department
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage = "Department name is required")]
        [StringLength(100, ErrorMessage = "Max length of the department name is 100 charter")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Department description is required")]
        [StringLength(500, ErrorMessage = "Max length of the department description is 500 charter")]
        public string Description { get; set; } = default!;
    }
}
