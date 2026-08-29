using ClinicManagement.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Specifications
{
    public class PatientByInfoSpecification : BaseSpecification<Patient, Guid>
    {
        public PatientByInfoSpecification(string phoneNumber, string email) : base(p => p.PhoneNumber == phoneNumber || p.Email == email)
        {

        }
    }
}
