using ClinicManagement.Application.Dtos.Patient;
using ClinicManagement.Application.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Mapping
{
    public static class PatientMapping
    {
        public static PatientDto ToDto(this Patient entity)
        {
            return new PatientDto()
            {
                Id = entity.Id,
                Email = entity.Email,
                Name = entity.Name,
                PhoneNumber = entity.PhoneNumber,
                Height = entity.Height,
                Weight = entity.Weight,
                BirthDate = entity.BirthDate,
            };
        }

        
    }
}
