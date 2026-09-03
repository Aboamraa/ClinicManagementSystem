using ClinicManagement.Application.Dtos.Department;
using ClinicManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Mapping
{
    public static class DepartmentMapping
    {
        public static DepartmentDto ToDto(this Department department)
            => new() { Id = department.Id, Name = department.Name, Description = department.Description };


        public static Department ToDepartment(this CreateDepartmentDto department)
            => new() { Name = department.Name, Description = department.Description };

    }
}
