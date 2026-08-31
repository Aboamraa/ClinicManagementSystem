using ClinicManagement.Application.Dtos.Appointment;
using ClinicManagement.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Specifications
{
    public class AppointmentSpecification : BaseSpecification<Appointment, Guid>
    {
        public AppointmentSpecification(AppointmentQueryParams queryParams) :
            base(a => (!queryParams.MinPrice.HasValue || a.Price >= queryParams.MinPrice)
            && (!queryParams.MaxPrice.HasValue || a.Price <= queryParams.MaxPrice)
            && (!queryParams.AfterDate.HasValue || a.StartTime >= queryParams.AfterDate)
            && (!queryParams.BeforeDate.HasValue || a.StartTime <= queryParams.BeforeDate)
            && (!queryParams.WithDoctor.HasValue || a.DoctorId == queryParams.WithDoctor)
            && (!queryParams.AppointmentStatus.HasValue || a.AppointmentStatus == queryParams.AppointmentStatus))
        {
            ApplyPaging((queryParams.PageNumber - 1) * queryParams.PageSize, queryParams.PageSize);
        }
    }
}
