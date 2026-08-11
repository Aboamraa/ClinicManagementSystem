using ClinicManagement.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Domain.Entities
{
    public class Doctor : User<int>
    {
        // Inherited
        // Id => int
        // Name 
        // Email 
        // PhoneNumber

        public int YearsOfExperience { get; set; }
        public decimal Salary { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = []; // Appointment Id => GUID

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
