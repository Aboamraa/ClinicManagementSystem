using ClinicManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Specifications
{
    public class DepartmentByNameSpecification : BaseSpecification<Department, int>
    {
        public DepartmentByNameSpecification(string departmentName) : base(d => d.Name == departmentName) { }
    }
}
