using ClinicManagement.Application.Entities.Abstract;
using ClinicManagement.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Domain.Entities
{
    public class Doctor : BaseEntity<int> // User<int>
    {
        //public static readonly Type key = typeof(int);
        // Inherited
        // Id => int
        // Name 
        // Email 
        // PhoneNumber


        // represents the foreign key to the User entity of the identity
        public Guid UserId { get; set; }
        public int YearsOfExperience { get; set; }
        public decimal Salary { get; set; }
        public string Name { get; set; } = default!;

        public ICollection<Appointment> Appointments { get; set; } = []; // Appointment Id => GUID

        public int DepartmentId { get; set; }
        public Department Department { get; set; } = default!;
    }
}
