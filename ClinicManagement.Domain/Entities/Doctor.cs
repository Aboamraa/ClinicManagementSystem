using ClinicManagement.Application.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Entities
{
    public class Doctor : User<int>
    {
        //public static readonly Type key = typeof(int);
        // Inherited
        // Id => int
        // Name 
        // Email 
        // PhoneNumber

        public int YearsOfExperience { get; set; }
        public decimal Salary { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = []; // Appointment Id => GUID

        public int DepartmentId { get; set; }
        public Department Department { get; set; } = default!;
    }
}
