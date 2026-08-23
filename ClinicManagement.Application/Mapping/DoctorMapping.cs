using ClinicManagement.Application.Dtos;
using ClinicManagement.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Mapping
{
    public static class DoctorMapping
    {
        public static DoctorDto ToDto(this Doctor doctorModel)
        {
            return new DoctorDto
            {
                Id = doctorModel.Id,
                Name = doctorModel.Name,
                Email = doctorModel.Email,
                PhoneNumber = doctorModel.PhoneNumber,
                YearsOfExperience = doctorModel.YearsOfExperience,
                DepartmentTitle = doctorModel.Department?.Name ?? ""
            };
        }
        public static Doctor DoctorDtoToDoctor(this DoctorDto doctorDto)
        {
            return new Doctor
            {
                Id = doctorDto.Id,
                Name = doctorDto.Name,
                Email = doctorDto.Email,
                PhoneNumber = doctorDto.PhoneNumber,
                YearsOfExperience = doctorDto.YearsOfExperience,
            };
        }

        public static Doctor ToDoctor(this CreateDoctorDto createDoctorDto)
        {
            return new Doctor
            {

                Name = createDoctorDto.Name,
                Email = createDoctorDto.Email,
                PhoneNumber = createDoctorDto.PhoneNumber,
                YearsOfExperience = createDoctorDto.YearsOfExperience,
                Salary = createDoctorDto.Salary,
                DepartmentId = createDoctorDto.DepartmentId
            };
        }
    }
}
