using ClinicManagement.Application.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Dtos.Appointment
{
    public class AppointmentQueryParams
    {
        public DateTime? BeforeDate { get; set; }
        public DateTime? AfterDate { get; set; }
        public int? WithDoctor { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public AppointmentStatus? AppointmentStatus { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
