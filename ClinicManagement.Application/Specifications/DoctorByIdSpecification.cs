using ClinicManagement.Application.Dtos;
using ClinicManagement.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Specifications
{
    public class DoctorByIdSpecification : BaseSpecification<Doctor, int>
    {
        public DoctorByIdSpecification(int doctorId) : base(d => d.Id == doctorId)
        {
            AddInclude(d => d.Department);
        }
    }
}
