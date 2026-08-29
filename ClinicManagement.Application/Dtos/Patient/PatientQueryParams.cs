using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Dtos.Patient
{
    public class PatientQueryParams
    {
        public double? MinHeight { get; set; }
        public double? MaxHeight { get; set; }
        public double? MinWeight { get; set; }
        public double? MaxWeight { get; set; }
        public DateOnly? BirthDateAfter { get; set; }
        public DateOnly? BirthDateBefore { get; set; }
        public bool OrderByWeight { get; set; }
        public bool OrderByWeightDec { get; set; }
        public bool OrderByHeight { get; set; }
        public bool OrderByHeightDec { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
