using ClinicManagement.Domain.Entities;
using ClinicManagement.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Infrastructure.Data.Configuration
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasOne(d => d.Department)
                .WithMany(dep => dep.Doctors)
                   .HasForeignKey(d => d.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(d => d.Salary)
                .HasColumnType("decimal(18,2)");

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
