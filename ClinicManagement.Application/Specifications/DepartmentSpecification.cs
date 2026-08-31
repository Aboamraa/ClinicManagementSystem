using ClinicManagement.Application.Dtos.Department;
using ClinicManagement.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Specifications
{
    public class DepartmentSpecification : BaseSpecification<Department, int>
    {
        public DepartmentSpecification(DepartmentQueryParams queryParams)
        {
            ApplyPaging((queryParams.PageSize * queryParams.PageNumber - 1), queryParams.PageSize);
        }
    }
}
