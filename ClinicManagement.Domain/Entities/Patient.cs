using ClinicManagement.Application.Entities.Abstract;
using ClinicManagement.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Domain.Entities
{
    public class Patient : BaseEntity<Guid> // User<Guid>
    {
        // # inherited # //
        // Id
        // Name
        // Email
        // PhoneNumber

        // represents the foreign key to the User entity of the identity
        public Guid UserId { get; set; }
        public string Name { get; set; } = default!;

        public double Weight { get; set; }
        public double Height { get; set; }
        public DateOnly BirthDate { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = [];

    }
}
