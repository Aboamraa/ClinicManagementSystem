using ClinicManagement.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Infrastructure.Data
{
    public class ClinicDbContext : DbContext
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
