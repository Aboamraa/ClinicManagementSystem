using ClinicManagement.Application.Dtos.Patient;
using ClinicManagement.Application.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Specifications
{
    public class PatientSpecification : BaseSpecification<Patient, Guid>
    {
        public PatientSpecification(PatientQueryParams patientQuery) : base(
            p =>
            (!patientQuery.MinHeight.HasValue || p.Height >= patientQuery.MinHeight.Value)
            &&
            (!patientQuery.MaxHeight.HasValue || p.Height <= patientQuery.MaxHeight.Value)
            &&
            (!patientQuery.MinWeight.HasValue || p.Weight >= patientQuery.MinWeight.Value)
            &&
            (!patientQuery.MaxWeight.HasValue || p.Weight <= patientQuery.MaxWeight.Value)
            &&
            (!patientQuery.BirthDateAfter.HasValue || p.BirthDate > patientQuery.BirthDateAfter.Value)
            &&
            (!patientQuery.BirthDateBefore.HasValue || p.BirthDate < patientQuery.BirthDateBefore.Value)
            )
        {
            if (patientQuery.OrderByWeightDec)
                AddOrderByDescending(p => p.Weight);
            else if (patientQuery.OrderByWeight)
                AddOrderBy(p => p.Weight);
            else if (patientQuery.OrderByHeightDec)
                AddOrderByDescending(p => p.Height);
            else if (patientQuery.OrderByHeight)
                AddOrderBy(p => p.Height);


            ApplyPaging((patientQuery.PageNumber - 1) * patientQuery.PageSize, patientQuery.PageSize);
        }
    }
}
