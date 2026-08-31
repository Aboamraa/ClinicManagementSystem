using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Dtos.Department
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;


        public string Description { get; set; } = default!;
    }
}
