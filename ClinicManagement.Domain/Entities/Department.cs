using ClinicManagement.Application.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Domain.Entities
{
    public class Department : BaseEntity<int>
    {
        // Inherited
        // Id => int

        public string Name { get; set; } =default!;
        public string Description { get; set; }=default!;


        public ICollection<Doctor> Doctors { get; set; } = [];
    }
}
