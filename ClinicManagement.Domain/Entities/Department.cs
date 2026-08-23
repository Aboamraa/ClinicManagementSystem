using ClinicManagement.Application.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Entities
{
    public class Department : BaseEntity<int>
    {
        // Inherited
        // Id => int

        public string Name { get; set; }
        public string Description { get; set; }


        public ICollection<Doctor> Doctors { get; set; } = [];
    }
}
