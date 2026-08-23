using ClinicManagement.Application.Dtos;
using ClinicManagement.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Specifications
{
    public class DoctorSpecification : BaseSpecification<Doctor, int>
    {
        public DoctorSpecification(DoctorQueryParams query) : base(d => !query.DepartmentId.HasValue || d.DepartmentId == query.DepartmentId.Value)
        {
            AddInclude(d => d.Department);

            AddOrderByDescending(d => d.YearsOfExperience);
            ApplyPaging((query.PageNumber - 1) * query.PageSize, query.PageSize);


        }
    }
}
