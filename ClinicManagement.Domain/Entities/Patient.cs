using ClinicManagement.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Domain.Entities
{
    public class Patient : User<Guid>
    {
        // # inherited # //
        // Id
        // Name
        // Email
        // PhoneNumber

        public double Weight { get; set; }
        public double Height { get; set; }
        public DateOnly BirthDate { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = [];

    }
}
